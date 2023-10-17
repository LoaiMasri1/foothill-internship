using JwtAuth.Options.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtAuth.DependencyInjection;

public static class AuthenticationExtentions
{
    public static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services
    )
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
