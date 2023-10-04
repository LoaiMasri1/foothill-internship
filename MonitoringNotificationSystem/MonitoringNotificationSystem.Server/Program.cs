using Microsoft.Extensions.Configuration;
using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Server;
using MonitoringNotificationSystem.Shared.Configurations;

var serverStaticsConfig = new ServerStatisticsConfig();

const string appSettingsFileName = "appsettings.json";
const string RabbitMQConnectionStringKey = "RabbitMQ";

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(appSettingsFileName, optional: true, reloadOnChange: true)
    .Build();

configuration.GetSection(nameof(ServerStatisticsConfig)).Bind(serverStaticsConfig);

var rabbitMQConnectionString = configuration.GetConnectionString(RabbitMQConnectionStringKey)!;

IMessageBroker messageBroker = new RabbitMQMessageBroker(rabbitMQConnectionString);

var serverStatisticsCollector = new ServerStatisticsCollector(serverStaticsConfig, messageBroker);

await serverStatisticsCollector.StartAsync();
