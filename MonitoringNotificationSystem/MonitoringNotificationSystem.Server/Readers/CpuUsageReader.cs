using System.Diagnostics;

namespace MonitoringNotificationSystem.Server.Readers;

public class CpuUsageReader
{
    public static double GetCpuUsage()
    {
        using var process = Process.GetCurrentProcess();

        var cpuUsage = process.TotalProcessorTime.TotalSeconds / Environment.ProcessorCount;

        return cpuUsage;
    }
}
