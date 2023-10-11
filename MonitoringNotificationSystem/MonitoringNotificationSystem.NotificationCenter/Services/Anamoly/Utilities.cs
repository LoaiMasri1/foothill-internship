namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly;

public static class Utilities
{
    public static partial class Anomaly
    {
        public static double CalculatePercentageChange(double previousValue, double currentValue)
        {
            if (previousValue == 0)
                return 0;

            return (currentValue - previousValue) / previousValue;
        }
    }
}
