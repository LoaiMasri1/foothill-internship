using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<IEnumerable<OrderItem>> ListOrderedMenuItemsAsync(int reservationId);
        Task<IEnumerable<OrderMenuItemResponse>> ListOrdersAndMenuItemsAsync(int reservationId);
        Task<OrderItem> UpdateOrderItemAsync(int id, OrderItem orderItem);
    }
}
