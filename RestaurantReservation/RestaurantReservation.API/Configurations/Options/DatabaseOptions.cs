namespace RestaurantReservation.API.Configurations.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; }
    public int MaxRetryDelay { get; }
    public bool EnableSensitiveDataLogging { get; }
    public bool EnableDetailedErrors { get; }
}
