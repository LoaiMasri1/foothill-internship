using Microsoft.AspNetCore.SignalR;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services;
using MonitoringNotificationSystem.NotificationCenter.Services.Anamoly;
using MonitoringNotificationSystem.NotificationCenter.Services.Anamoly.Strategies;

namespace MonitoringNotificationSystem.NotificationCenter.DependencyInjection;

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
