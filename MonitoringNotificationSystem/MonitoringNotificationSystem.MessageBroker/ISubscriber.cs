namespace MonitoringNotificationSystem.MessageBroker;

public interface ISubscriber
{
    void Subscribe<T>(string topicName, string routingKeyPattern, Action<T> onMessageReceived);
}
