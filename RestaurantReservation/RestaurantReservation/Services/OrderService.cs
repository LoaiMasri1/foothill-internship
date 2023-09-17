using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
    {
        var newOrder = new Order
        {
            EmployeeId = orderRequest.EmployeeId,
            ReservationId = orderRequest.ReservationId,
            OrderDate = orderRequest.OrderDate,
            TotalAmount = orderRequest.TotalAmount
        };

        await _orderRepository.CreateOrderAsync(newOrder);
        var response = new OrderResponse(
            newOrder.OrderId,
            newOrder.EmployeeId,
            newOrder.ReservationId,
            newOrder.OrderDate,
            newOrder.TotalAmount);

        return response;
    }

    public async Task<OrderResponse> UpdateOrderAsync(int id, OrderRequest orderRequest)
    {
        var updatedOrder = new Order
        {
            EmployeeId = orderRequest.EmployeeId,
            ReservationId = orderRequest.ReservationId,
            OrderDate = orderRequest.OrderDate,
            TotalAmount = orderRequest.TotalAmount
        };

        var order = await _orderRepository
            .UpdateOrderAsync(id, updatedOrder);

        var response = new OrderResponse(
            order.OrderId,
            order.EmployeeId,
            order.ReservationId,
            order.OrderDate,
            order.TotalAmount);

        return response;
    }

    public async Task<double> CalculateAverageOrderAmountAsync(int employeeId)
    {
        var orders = await _orderRepository
            .CalculateAverageOrderAmountAsync(employeeId);

        return orders;
    }
}
