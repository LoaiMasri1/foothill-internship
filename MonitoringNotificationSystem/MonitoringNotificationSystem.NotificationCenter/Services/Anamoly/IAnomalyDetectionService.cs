using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services;

public interface IAnomalyDetectionService
{
    Task CheckAndSendAnomalyAlertsAsync(ServerStatistics statistics);
    Task SendAlertAsync(string message);
}
