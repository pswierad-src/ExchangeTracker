using Microsoft.Extensions.DependencyInjection;

namespace ExchangeMongo;

public static class IoCBootstrapper
{
    public static void ConfigureMongo(this IServiceCollection services)
    {
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}