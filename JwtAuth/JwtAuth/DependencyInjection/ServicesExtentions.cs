using JwtAuth.Features.Auth.Services;
using JwtAuth.Features.Users.Repositories;

namespace JwtAuth.DependencyInjection;

public static class ServicesExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();

        return services;
    }
}
