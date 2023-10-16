using JwtAuth.Options.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtAuth.DependencyInjection;

public static class AuthenticationExtentions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
            .AddJwtConfigurations()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }

    private static IServiceCollection AddJwtConfigurations(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJwtOptions>();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        return services;
    }
}
