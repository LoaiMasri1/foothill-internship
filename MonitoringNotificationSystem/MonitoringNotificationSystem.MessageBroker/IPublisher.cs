namespace MonitoringNotificationSystem.MessageBroker;

public interface IPublisher
{
    void Publish<T>(string topicName, string routingKey, T message);
}
