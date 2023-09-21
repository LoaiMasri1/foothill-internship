using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateOrderAsync(int id, Order order)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Order with id {id} does not exist");
        }
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id)
            ?? throw new NotFoundException($"Order with id {id} does not exist");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Orders.AnyAsync(x => x.OrderId == id);
        return exists;
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
