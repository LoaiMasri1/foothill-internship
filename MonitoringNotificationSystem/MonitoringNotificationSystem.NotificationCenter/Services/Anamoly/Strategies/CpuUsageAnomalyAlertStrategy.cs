using MonitoringNotificationSystem.NotificationProcessor;
using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly.Strategies;

public class CpuUsageAnomalyAlertStrategy : IAnomalyAlertStrategy
{
    public bool IsAlert(ServerStatistics statistics, ServerStatistics previousStatistics) =>
        Utilities.Anomaly.CalculatePercentageChange(
            previousStatistics.CpuUsage,
            statistics.CpuUsage
        ) > EnviromentVeriables.CpuUsageAnomalyThresholdPercentage;

    public string GetAlertMessage() => "Anomaly Alert: Sudden increase in CPU Usage!";

    public bool IsAlert(ServerStatistics statistics)
    {
        throw new NotImplementedException();
    }
}
