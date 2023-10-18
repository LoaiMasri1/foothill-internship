using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<double> CalculateAverageOrderAmountAsync(int employeeId);
        Task<Order> CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<Order> UpdateOrderAsync(int id, Order order);
    }
}
