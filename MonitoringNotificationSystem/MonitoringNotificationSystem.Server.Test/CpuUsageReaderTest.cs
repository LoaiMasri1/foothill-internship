using MonitoringNotificationSystem.Server.Readers;

namespace MonitoringNotificationSystem.Server.Test;

public class CpuUsageReaderTest
{
    [Fact]
    public void GetCpuUsage_ReturnsPositiveValue()
    {
        double cpuUsage = CpuUsageReader.GetCpuUsage();
        Assert.True(cpuUsage > 0);
    }
}
