namespace MonitoringNotificationSystem.Server;

public static class Utilities
{
    public static void PeriodicServerStatisticsPublisherAsync(
        ServerStatisticsCollector collector,
        int samplingIntervalSeconds
    )
    {
        System.Timers.Timer timer =
            new(interval: TimeSpan.FromSeconds(samplingIntervalSeconds).TotalMilliseconds);

        bool isPublishing = false;

        timer.Elapsed += async (sender, e) =>
        {
            if (isPublishing)
                return;

            var signalTime = e.SignalTime;

            Console.WriteLine($"Publishing server statistics at {signalTime}");

            isPublishing = true;
            await collector.PublishServerStatisticsAsync();
            isPublishing = false;
        };
        timer.Start();

        Console.WriteLine(
            $"Started periodic server statistics publisher with interval {samplingIntervalSeconds} seconds"
        );

        while (true)
            Thread.Sleep(1000);
    }
}
