using Microsoft.AspNetCore.SignalR;
using MonitoringNotificationSystem.NotificationProcessor.Hubs;
using MonitoringNotificationSystem.NotificationProcessor.Repositories;
using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly;
using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly.Strategies;

namespace MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;

public static class ServiceExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAnomalyDetectionService>(provider =>
        {
            var hub = provider.GetRequiredService<
                IHubContext<NotificationHub, IStatisticsClient>
            >();
            var repository = provider.GetRequiredService<INotificationRepository>();

            var alertStrategies = new List<IAnomalyAlertStrategy>()
            {
                new CpuHighUsageAlertStrategy(),
                new CpuUsageAnomalyAlertStrategy(),
                new MemoryHighUsageAlertStrategy(),
                new MemoryUsageAnomalyAlertStrategy(),
            };

            return new AnomalyDetectionService(hub, alertStrategies, repository);
        });

        services.AddSingleton<Connector>();

        services.AddHostedService<BackgroundWorkerService>();

        return services;
    }
}
