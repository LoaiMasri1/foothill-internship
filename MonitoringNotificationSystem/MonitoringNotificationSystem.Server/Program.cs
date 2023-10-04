using Microsoft.Extensions.Configuration;
using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Server;
using MonitoringNotificationSystem.Shared.Configurations;

var rabbitMQUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
var rabbitMQPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");

var serverStaticsConfig = new ServerStatisticsConfig();

const string appSettingsFileName = "appsettings.json";

var rabbitMQConnectionString = $"amqp://{rabbitMQUser}:{rabbitMQPassword}@{rabbitMQHost}";

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(appSettingsFileName, optional: true, reloadOnChange: true)
    .Build();

configuration.GetSection(nameof(ServerStatisticsConfig)).Bind(serverStaticsConfig);

IMessageBroker messageBroker = new RabbitMQMessageBroker(rabbitMQConnectionString);

var serverStatisticsCollector = new ServerStatisticsCollector(serverStaticsConfig, messageBroker);

await serverStatisticsCollector.StartAsync();
