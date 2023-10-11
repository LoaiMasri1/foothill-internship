using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly;

public interface IAnomalyDetectionService
{
    Task CheckAndSendAnomalyAlertsAsync(ServerStatistics statistics);
    Task SendAlertAsync(string message);
}
