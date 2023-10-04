using Microsoft.Extensions.Options;
using MonitoringNotificationSystem.Shared.Configurations;

namespace MonitoringNotificationSystem.NotificationCenter.Setups;
public class MongoDBConfigSetup : IConfigureOptions<MongoDBConfig>
{
    private readonly IConfiguration _configuration;
    private const string _sectionName = "MongoDBConfig";

    public MongoDBConfigSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MongoDBConfig options)
    {
        options.ConnectionString = _configuration.GetConnectionString(_sectionName)!;

        _configuration.GetSection(nameof(MongoDBConfig)).Bind(options);
    }
}