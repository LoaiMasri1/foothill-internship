using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MonitoringNotificationSystem.Server;

public class MemoryUsageReader
{
    public static float GetAvailableMemory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            string availMemInKB = GetMemoryInfo("MemAvailable:");
            float availMemInMB = float.Parse(availMemInKB) / 1024f;
            return availMemInMB;
        }

        PerformanceCounter availableMemoryCounter = new("Memory", "Available Bytes");
        float availableMemoryBytes = availableMemoryCounter.NextValue();
        float availableMemoryMB = availableMemoryBytes / (1024f * 1024f);
        return availableMemoryMB;
    }

    public static float GetMemoryUsage()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            string totalMemInKB = GetMemoryInfo("MemTotal:");
            string freeMemInKB = GetMemoryInfo("MemFree:");
            float totalMemInMB = float.Parse(totalMemInKB) / 1024f;
            float freeMemInMB = float.Parse(freeMemInKB) / 1024f;
            float usedMemInMB = totalMemInMB - freeMemInMB;
            return usedMemInMB;
        }

        PerformanceCounter ramCounter = new("Memory", "Available MBytes");
        float availableMemoryMB = ramCounter.NextValue();
        return availableMemoryMB;
    }

    private static string GetMemoryInfo(string filter)
    {
        string memInfoPath = "/proc/meminfo";
        string memInfo = File.ReadAllText(memInfoPath);
        string[] lines = memInfo.Split('\n');
        string availMemLine = lines.FirstOrDefault(x => x.StartsWith(filter))!;
        string[] availMemLineParts = availMemLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string memResource = availMemLineParts[1];
        return memResource;
    }
}
