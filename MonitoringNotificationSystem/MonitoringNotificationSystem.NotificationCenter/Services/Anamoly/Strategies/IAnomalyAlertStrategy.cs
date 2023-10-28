using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly.Strategies;

public interface IAnomalyAlertStrategy
{
    bool IsAlert(ServerStatistics statistics, ServerStatistics previousStatistics);
    string GetAlertMessage();
}
