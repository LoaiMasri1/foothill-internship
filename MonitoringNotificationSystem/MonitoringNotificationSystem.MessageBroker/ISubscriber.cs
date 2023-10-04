namespace MonitoringNotificationSystem.MessageBroker;

public interface ISubscriber
{
    Task SubscribeAsync<T>(string topicName, string routingKeyPattern, Action<T> onMessageReceived);
}
