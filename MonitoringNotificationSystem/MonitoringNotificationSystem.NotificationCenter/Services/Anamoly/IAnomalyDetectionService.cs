using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly;

public interface IAnomalyDetectionService
{
    Task CheckAndSendAnomalyAlertsAsync(ServerStatistics statistics);
    Task SendAlertAsync(string message);
}
