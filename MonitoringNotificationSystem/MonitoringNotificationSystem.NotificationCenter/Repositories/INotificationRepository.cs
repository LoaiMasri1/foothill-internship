using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Repositories;

public interface INotificationRepository
{
    Task SaveStatisticsAsync(ServerStatistics statistics);
}
