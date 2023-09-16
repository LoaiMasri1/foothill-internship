using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderService
{
    private readonly RestaurantReservationDbContext _context;

    public OrderService(RestaurantReservationDbContext context)
    {
        _context= context;
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

        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();

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
        var order = await _context.Orders.FindAsync(id)
            ?? throw new NotFoundException($"Order with id {id} does not exist");

        var isEmployeeExist = await _context.Employees
            .AnyAsync(x => x.EmployeeId == orderRequest.EmployeeId);

        if (!isEmployeeExist)
        {
            throw new NotFoundException($"Employee with id {orderRequest.EmployeeId} does not exist");
        }

        order.EmployeeId = orderRequest.EmployeeId;
        order.OrderDate = orderRequest.OrderDate;
        order.TotalAmount = orderRequest.TotalAmount;

        await _context.SaveChangesAsync();

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
        var orders = await _context.Orders
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();

        var averageOrderAmount = orders
            .Average(x => x.TotalAmount);

        return averageOrderAmount;
    }
}
