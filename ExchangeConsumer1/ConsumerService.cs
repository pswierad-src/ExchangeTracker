using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using ExchangeMongo;
using ExchangeMongo.Documents;
using ExchangeMongo.Extensions;
using ExchangeShared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExchangeConsumer1;

public class ConsumerService : IConsumerService
{
    private readonly string exchangeName = "ExchangeTracker";
    private readonly IMongoRepository<TradeDocument> _tradeDocumentRepository;

    public ConsumerService(IMongoRepository<TradeDocument> tradeDocumentRepository)
    {
        _tradeDocumentRepository = tradeDocumentRepository;
    }

    public void Run()
    {
        var factory = new ConnectionFactory() {HostName = "localhost"};

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
        var queueName = channel.QueueDeclare().QueueName;
        
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: Enum.GetName(TradeType.Currency));
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: Enum.GetName(TradeType.Cryptocurrency));
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: Enum.GetName(TradeType.Stocks));
        
        Console.WriteLine($"Waiting for {Enum.GetName(TradeType.Currency)}, {Enum.GetName(TradeType.Cryptocurrency)}, {Enum.GetName(TradeType.Stocks)} exchanges...");
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var trade = JsonSerializer.Deserialize<Trade>(message);

            if (!_tradeDocumentRepository.AnyAsync(x => x.Id == trade.ExchangeId).Result)
            {
                _tradeDocumentRepository.AddAsync(trade.ToDocument());    
                Console.WriteLine($"Handled exchange {trade.ExchangeId} for {trade.Type} - {trade.ExchangedProduct}");
            }
        };
        
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        Console.ReadLine();
    }
}