using Carter;

namespace JwtAuth.DependencyInjection;

public static class ConfigureDependencies
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddCarter().AddSwagger().AddAuthenticationAndAuthorization().AddServices();

        return services;
    }
}
