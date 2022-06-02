using ExchangePublisher;
using ExchangeShared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddTransient<IPublisherService, PublisherService>();
        services.AddTransient<ITradeProvider, TradeProvider>();
    }).Build();

var service = ActivatorUtilities.CreateInstance<PublisherService>(host.Services);
service.Run();