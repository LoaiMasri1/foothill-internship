using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services;

public interface IAnomalyDetectionService
{
    void CheckAndSendAnomalyAlerts(ServerStatistics statistics);
}
