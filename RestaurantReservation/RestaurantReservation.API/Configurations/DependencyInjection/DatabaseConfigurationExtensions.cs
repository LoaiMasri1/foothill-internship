using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantReservation.API.Configurations.Options.Configurations;
using RestaurantReservation.API.Configurations.Options;
using RestaurantReservation.Db;

namespace RestaurantReservation.API.Configurations.DependencyInjection;

public static class DatabaseConfigurationExtensions
{
    public static IServiceCollection AddResturantReservationDbContext(
        this IServiceCollection services
    )
    {
        services.ConfigureOptions<ConfigureDatabaseOptions>();

        services.AddDbContext<RestaurantReservationDbContext>(
            (sp, options) =>
            {
                var databaseOption = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options
                    .UseSqlServer(
                        databaseOption.ConnectionString,
                        opt =>
                            opt.EnableRetryOnFailure(
                                maxRetryCount: databaseOption.MaxRetryCount,
                                maxRetryDelay: TimeSpan.FromSeconds(databaseOption.MaxRetryDelay),
                                errorNumbersToAdd: null
                            )
                    )
                    .EnableSensitiveDataLogging(databaseOption.EnableSensitiveDataLogging)
                    .EnableDetailedErrors(databaseOption.EnableDetailedErrors);
            }
        );

        return services;
    }
}
