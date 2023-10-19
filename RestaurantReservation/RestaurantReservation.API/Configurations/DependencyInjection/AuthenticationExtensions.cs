using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantReservation.API.Configurations.Options.Configurations;

namespace RestaurantReservation.API.Configurations.DependencyInjection;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.AddJwtConfigurations();
        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddJwtConfigurations(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJwtOptions>();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        return services;
    }
}
