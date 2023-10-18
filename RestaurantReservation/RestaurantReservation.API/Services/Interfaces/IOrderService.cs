using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<double> CalculateAverageOrderAmountAsync(int employeeId);
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);
        Task<OrderResponse> UpdateOrderAsync(int id, OrderRequest orderRequest);
    }
}
