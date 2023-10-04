namespace MonitoringNotificationSystem.NotificationCenter.Hubs;

public interface IStatisticsClient
{
    Task ReceiveMessage(string message);
    Task AnomalyMessage(string message);
}
