namespace RestaurantReservation.API.Configurations.DependencyInjection;

public static class ConfigureDependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddServices()
            .AddController()
            .AddMapper()
            .AddSwagger()
            .AddResturantReservationDbContext();

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection AddController(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
