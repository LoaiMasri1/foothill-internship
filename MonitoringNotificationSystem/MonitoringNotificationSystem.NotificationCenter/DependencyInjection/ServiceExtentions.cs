using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly;
using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly.Strategies;

namespace MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;

public static class ServiceExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAnomalyAlertStrategy, CpuHighUsageAlertStrategy>();
        services.AddSingleton<IAnomalyAlertStrategy, CpuUsageAnomalyAlertStrategy>();
        services.AddSingleton<IAnomalyAlertStrategy, MemoryHighUsageAlertStrategy>();
        services.AddSingleton<IAnomalyAlertStrategy, MemoryUsageAnomalyAlertStrategy>();

        services.AddSingleton<IAnomalyDetectionService, AnomalyDetectionService>();

        services.AddSingleton<IConnector, Connector>();

        services.AddHostedService<BackgroundWorkerService>();

        return services;
    }
}
