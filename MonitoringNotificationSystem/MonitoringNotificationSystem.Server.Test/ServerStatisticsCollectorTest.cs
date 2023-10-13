using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.Server.Test;

public class ServerStatisticsCollectorTest
{
    private readonly ServerStatisticsCollector _collector;
    private readonly Mock<IMessageBroker> _messageBroker;

    private const string serverIdentifier = "TestServer";
    private const string topicName = $"ServerStatistics.{serverIdentifier}";

    public ServerStatisticsCollectorTest()
    {
        _messageBroker = new Mock<IMessageBroker>();
        _collector = new ServerStatisticsCollector(serverIdentifier, _messageBroker.Object);
    }

    [Fact]
    public async Task StartAsync_PublishesServerStatistics()
    {
        await _collector.PublishServerStatisticsAsync();

        _messageBroker.Verify(
            x => x.Publish(topicName, It.IsAny<ServerStatistics>()),
            Times.AtLeastOnce
        );
    }

    [Fact]
    public async Task StartAsync_PublishesServerStatistics_WithCorrectValues()
    {
        await _collector.PublishServerStatisticsAsync();

        await Task.Delay(TimeSpan.FromSeconds(2));

        _messageBroker.Verify(
            x =>
                x.Publish(
                    topicName,
                    It.Is<ServerStatistics>(
                        y => y.CpuUsage > 0 && y.MemoryUsage > 0 && y.AvailableMemory > 0
                    )
                ),
            Times.AtLeastOnce
        );
    }

    [Fact]
    public async Task StartAsync_PublishesServerStatistics_WithCorrectTimestamp()
    {
        await _collector.PublishServerStatisticsAsync();

        _messageBroker.Verify(
            x =>
                x.Publish(
                    topicName,
                    It.Is<ServerStatistics>(
                        y =>
                            y.Timestamp > DateTime.UtcNow.AddSeconds(-2)
                            && y.Timestamp < DateTime.UtcNow.AddSeconds(2)
                    )
                ),
            Times.AtLeastOnce
        );
    }
}
