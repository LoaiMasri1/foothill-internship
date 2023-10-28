using MonitoringNotificationSystem.Server.Readers;
using System.Runtime.InteropServices;

namespace MonitoringNotificationSystem.Server.Test;

public class MemoryUsageReaderTest
{
    [Fact]
    public void GetAvailableMemory_ReturnsPositiveValue_Windows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        float availableMemory = MemoryUsageReader.GetAvailableMemory();
        Assert.True(availableMemory > 0);
    }

    [Fact]
    public void GetMemoryUsage_ReturnsPositiveValue_Windows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        float memoryUsage = MemoryUsageReader.GetMemoryUsage();
        Assert.True(memoryUsage > 0);
    }

    [Fact]
    public void GetAvailableMemory_ReturnsPositiveValue_Linux()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return;
        }

        float availableMemory = MemoryUsageReader.GetAvailableMemory();
        Assert.True(availableMemory > 0);
    }

    [Fact]
    public void GetMemoryUsage_ReturnsPositiveValue_Linux()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return;
        }

        float memoryUsage = MemoryUsageReader.GetMemoryUsage();
        Assert.True(memoryUsage > 0);
    }
}
