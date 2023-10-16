namespace JwtAuth.DependencyInjection;

public static class ConfigureDependencies
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddAuth();

        return services;
    }
}
