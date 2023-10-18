using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemResponse> CreateOrderItemAsync(OrderItemRequest orderItemRequest);
        Task DeleteOrderItemAsync(int id);
        Task<IEnumerable<MenuItemResponse>> ListOrderedMenuItemsAsync(int reservationId);
        Task<IEnumerable<OrderMenuItemResponse>> ListOrdersAndMenuItemsAsync(int reservationId);
        Task<OrderItemResponse> UpdateOrderItemAsync(int id, OrderItemRequest orderItemRequest);
    }
}
