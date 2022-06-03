using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using ExchangeShared;
using RabbitMQ.Client;

namespace ExchangePublisher;

public class PublisherService : IPublisherService
{
    private readonly ITradeProvider _tradeProvider;
    private readonly string exchangeName = "ExchangeTracker"; 

    public PublisherService(ITradeProvider tradeProvider)
    {
        _tradeProvider = tradeProvider;
    }

    public void Run()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        Console.WriteLine("Please specify how many publisher workers you want to initiate");

        if (!int.TryParse(Console.ReadLine(), result: out var numberOfWorkers))
        {
            return;
        }

        Parallel.For(0, numberOfWorkers, index =>
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
            
            while (true)
            {
                var trade = _tradeProvider.GenerateTrade();

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(trade));
                channel.BasicPublish(exchange: exchangeName,
                    routingKey: Enum.GetName(trade.Type),
                    basicProperties: null,
                    body: body);

                Console.WriteLine($"Published {trade.Type} exchange for {trade.ExchangedProduct} on {trade.DateOfOperation}. Publishing worker: {index}");

            }
        });
    }
}