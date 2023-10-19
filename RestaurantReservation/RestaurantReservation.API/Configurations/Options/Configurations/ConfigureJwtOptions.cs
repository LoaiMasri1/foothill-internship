using Microsoft.Extensions.Options;

namespace RestaurantReservation.API.Configurations.Options.Configurations;
public class ConfigureJwtOptions : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureJwtOptions(IConfiguration configuration) => _configuration = configuration;

    public void Configure(JwtOptions options) =>
        _configuration.GetSection(JwtOptions.SectionName).Bind(options);
}
