using Microsoft.Extensions.DependencyInjection;

namespace ExchangeMongo;

public static class IoCBootstrapper
{
    public static void RegisterMongo(this IServiceCollection services)
    {
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}