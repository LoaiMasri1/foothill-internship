using RestaurantReservation.API.Services;
using RestaurantReservation.API.Services.Interfaces;

namespace RestaurantReservation.API.Configurations.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IResturantService, ResturantService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();

        return services;
    }
}
