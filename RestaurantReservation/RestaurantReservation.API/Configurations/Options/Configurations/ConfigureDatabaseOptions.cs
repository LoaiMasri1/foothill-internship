using Microsoft.Extensions.Options;

namespace RestaurantReservation.API.Configurations.Options.Configurations;

public class ConfigureDatabaseOptions : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;
    private const string SectionName = "Database";

    public ConfigureDatabaseOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString(SectionName)!;

        _configuration.GetSection(SectionName).Bind(options);
    }
}
