using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace MonitoringNotificationSystem.MessageBroker;

public class RabbitMQMessageBroker : IMessageBroker
{
    private readonly string _connectionString;
    private readonly IModel _channel;
    private readonly int _maxRetryCount;
    private readonly int _delayBetweenRetries;

    public RabbitMQMessageBroker(
        string connectionString,
        int maxRetryCount = 5,
        int delayBetweenRetries = 10
    )
    {
        _connectionString = connectionString;
        _maxRetryCount = maxRetryCount;
        _delayBetweenRetries = delayBetweenRetries;
        _channel = CreateModel();
    }

    public async Task SubscribeAsync<T>(string topicName, Func<T, Task> onMessageReceived)
    {
        _channel.ExchangeDeclare(exchange: topicName, type: ExchangeType.Topic);

        var queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(queue: queueName, exchange: topicName, routingKey: topicName);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var deserializedMessage = JsonSerializer.Deserialize<T>(message);

            await onMessageReceived(deserializedMessage!);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }

    public void Publish<T>(string topicName, T message)
    {
        _channel.ExchangeDeclare(exchange: topicName, type: ExchangeType.Topic);

        var serializedMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);

        _channel.BasicPublish(
            exchange: topicName,
            routingKey: topicName,
            basicProperties: null,
            body: body
        );
    }

    private IConnection CreateConnectionWithRetry()
    {
        var policy = Utilities.Polly.CreateRetryPolicy<BrokerUnreachableException>(
            _maxRetryCount,
            delayBetweenRetries: TimeSpan.FromSeconds(_delayBetweenRetries)
        );

        return policy.Execute(() => Utilities.RabbitMQ.CreateConnection(_connectionString));
    }

    private IModel CreateModel() => CreateConnectionWithRetry().CreateModel();
}
