namespace RestaurantReservation.API.Configurations.DependencyInjection;

using FluentValidation;
using RestaurantReservation.API.Configurations;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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
            .AddResturantReservationDbContext()
            .AddValidator();

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

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidationAutoValidation(options =>
        options.OverrideDefaultResultFactoryWith<CustomValidationResultFactory>());

        return services;
    }
}
