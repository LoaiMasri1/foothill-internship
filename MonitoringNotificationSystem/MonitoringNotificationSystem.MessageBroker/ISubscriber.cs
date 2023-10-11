namespace MonitoringNotificationSystem.MessageBroker;

public interface ISubscriber
{
    Task SubscribeAsync<TMessage>(string topicName, Func<TMessage, Task> onMessageReceived);
}
