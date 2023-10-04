﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services;
using MonitoringNotificationSystem.Shared.Configurations;
using MonitoringNotificationSystem.Shared.Data;
using System.Text.Json;

namespace MonitoringNotificationSystem.NotificationCenter;

public class Connector
{
    private readonly IHubContext<NotificationHub, IStatisticsClient> _hub;
    private readonly ServerStatisticsConfig _serverStatisticsConfig;
    private readonly ILogger<Connector> _logger;
    private readonly RabbitMQMessageBroker _broker;
    private readonly INotificationRepository _notificationRepository;
    private readonly IAnomalyDetectionService _anomalyDetectionService;

    public Connector(
        IHubContext<NotificationHub, IStatisticsClient> hubContext,
        IOptions<ServerStatisticsConfig> serverStatisticsConfig,
        ILogger<Connector> logger,
        INotificationRepository notificationRepository,
        IAnomalyDetectionService anomalyDetectionService
    )
    {
        var rabbitMQUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
        var rabbitMQPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");

        var rabbitMQConnectionString =
            $"amqp://{rabbitMQUser}:{rabbitMQPassword}@{rabbitMQHost}";
        _hub = hubContext;
        _serverStatisticsConfig = serverStatisticsConfig.Value;
        _logger = logger;
        _broker = new RabbitMQMessageBroker(rabbitMQConnectionString);
        _notificationRepository = notificationRepository;
        _anomalyDetectionService = anomalyDetectionService;
    }

    public async Task StartAsync()
    {
        var topic = $"ServerStatistics.{_serverStatisticsConfig.ServerIdentifier}";
        _logger.LogInformation("Starting connector {}", topic);
        try
        {
            await _broker.SubscribeAsync<ServerStatistics>(
                topic,
                topic,
                async (statistics) =>
                {
                    _logger.LogInformation(
                        "Sending statistics to clients {}",
                        _serverStatisticsConfig.SamplingIntervalSeconds
                    );
                    await _hub.Clients.All.ReceiveMessage(JsonSerializer.Serialize(statistics));
                    await _notificationRepository.SaveStatisticsAsync(statistics);
                    _anomalyDetectionService.CheckAndSendAnomalyAlerts(statistics);
                }
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting statistics");
        }
    }
}
