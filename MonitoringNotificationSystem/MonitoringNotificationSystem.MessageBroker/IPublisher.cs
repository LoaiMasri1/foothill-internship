namespace MonitoringNotificationSystem.MessageBroker;

public interface IPublisher
{
    void Publish<TMessage>(string topicName, TMessage message);
}
