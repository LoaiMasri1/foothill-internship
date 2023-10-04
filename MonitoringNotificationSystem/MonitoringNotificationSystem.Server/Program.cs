using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Server;
using MonitoringNotificationSystem.Shared.Configurations;

var rabbitMQUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
var rabbitMQPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");

var serverIdentifier = Environment.GetEnvironmentVariable("SERVER_IDENTIFIER")!;
var sampleIntervalSeconds = int.Parse(
    Environment.GetEnvironmentVariable("SAMPLING_INTERVAL_SECONDS")!
);

var serverStaticsConfig = new ServerStatisticsConfig
{
    ServerIdentifier = serverIdentifier,
    SamplingIntervalSeconds = sampleIntervalSeconds
};

var rabbitMQConnectionString = $"amqp://{rabbitMQUser}:{rabbitMQPassword}@{rabbitMQHost}";

IMessageBroker messageBroker = new RabbitMQMessageBroker(rabbitMQConnectionString);

var serverStatisticsCollector = new ServerStatisticsCollector(serverStaticsConfig, messageBroker);

await serverStatisticsCollector.StartAsync();
