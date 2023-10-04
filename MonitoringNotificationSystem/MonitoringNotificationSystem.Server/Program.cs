using Microsoft.Extensions.Configuration;
using MonitoringNotificationSystem.Shared.Configurations;

var serverStaticsConf = new ServerStatisticsConfig();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

configuration.GetSection(nameof(ServerStatisticsConfig)).Bind(serverStaticsConf);