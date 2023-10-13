using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MonitoringNotificationSystem.Shared.Configurations;

namespace MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;

public static class MongoExtentions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(EnviromentVeriables.mongoUrl));

        services.AddSingleton(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(EnviromentVeriables.mongoDB);
        });

        return services;
    }
}
