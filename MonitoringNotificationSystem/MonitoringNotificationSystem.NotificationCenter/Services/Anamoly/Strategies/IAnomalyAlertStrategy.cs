using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly.Strategies;

public interface IAnomalyAlertStrategy
{
    bool IsAlert(ServerStatistics statistics, ServerStatistics previousStatistics);
    string GetAlertMessage();
}
