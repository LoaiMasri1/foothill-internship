namespace MonitoringNotificationSystem.NotificationProcessor.Hubs;

public interface IStatisticsClient
{
    Task ReceiveMessage(string message);
    Task AnomalyMessage(string message);
}
