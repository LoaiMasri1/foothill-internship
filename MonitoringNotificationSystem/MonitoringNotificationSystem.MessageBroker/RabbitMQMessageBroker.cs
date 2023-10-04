using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MonitoringNotificationSystem.MessageBroker;

public class RabbitMQMessageBroker : IMessageBroker
{
    private readonly string _connectionString;
    private readonly IModel _channel;

    public RabbitMQMessageBroker(string connectionString)
    {
        _connectionString = connectionString;
        _channel = CreateConnection().CreateModel();
    }

    public async Task SubscribeAsync<T>(
        string topicName,
        string routingKeyPattern,
        Action<T> onMessageReceived
    )
    {
        _channel.ExchangeDeclare(exchange: topicName, type: ExchangeType.Topic);

        var queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(queue: queueName, exchange: topicName, routingKey: routingKeyPattern);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var deserializedMessage = JsonSerializer.Deserialize<T>(message);

            onMessageReceived(deserializedMessage!);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        await Task.Delay(Timeout.Infinite);
    }

    public void Publish<T>(string topicName, string routingKey, T message)
    {
        _channel.ExchangeDeclare(exchange: topicName, type: ExchangeType.Topic);

        var serializedMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);

        _channel.BasicPublish(
            exchange: topicName,
            routingKey: routingKey,
            basicProperties: null,
            body: body
        );
    }

    private IConnection CreateConnection()
    {
        var factory = new ConnectionFactory { Uri = new Uri(_connectionString) };

        try
        {
            return factory.CreateConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
