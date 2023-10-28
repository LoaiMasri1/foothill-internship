using MongoDB.Driver;
using MonitoringNotificationSystem.NotificationProcessor.Repositories;

namespace MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;

public static class RepositoryExtentions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<INotificationRepository>(provider =>
        {
            var database = provider.GetRequiredService<IMongoDatabase>();
            var collection = database.GetCollection<MongoServerStatistics>("statistics");

            return new NotificationRepository(collection);
        });

        return services;
    }
}
