using ExchangeConsumer2;
using ExchangeMongo;
using ExchangeShared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddTransient<IConsumerService, ConsumerService>();
        services.AddTransient<ITradeProvider, TradeProvider>();
        
        services.ConfigureMongo();
    }).Build();

var service = ActivatorUtilities.CreateInstance<ConsumerService>(host.Services);
service.Run();