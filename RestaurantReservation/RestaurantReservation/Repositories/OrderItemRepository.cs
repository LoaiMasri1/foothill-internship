using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Repositories;

public class OrderItemRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();

        return orderItem;
    }

    public async Task<OrderItem> UpdateOrderItemAsync(int id, OrderItem orderItem)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"OrderItem with id {id} does not exist");
        }
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();

        return orderItem;
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id)
            ?? throw new NotFoundException($"OrderItem with id {id} does not exist");

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.OrderItems.AnyAsync(x => x.OrderItemId == id);
        return exists;
    }

    public async Task<IEnumerable<OrderMenuItemResponse>> ListOrdersAndMenuItemsAsync(int reservationId)
    {
        var orderItemsGrouped = await _context.OrderItems
            .Include(x => x.Order)
            .Include(x => x.Item)
            .Where(x => x.Order.ReservationId == reservationId)
            .GroupBy(x => x.OrderId)
            .ToListAsync();

        var response = orderItemsGrouped.Select(orderGroup => new OrderMenuItemResponse
        (
            orderGroup.Key,
            orderGroup.Select(x => new MenuItemResponse(
                x.Item.ItemId,
                x.Item.ResturantId,
               x.Item.Name,
                x.Item.Description,
               x.Item.Price
                )).ToList(),
            orderGroup.Sum(x => x.Quantity)
            )
        );

        return response;
    }

    public async Task<IEnumerable<OrderItem>> ListOrderedMenuItemsAsync(int reservationId)
    {
        var orderItems = await _context.OrderItems
            .Include(x => x.Order)
            .Include(x => x.Item)
            .Where(x => x.Order.ReservationId == reservationId)
            .ToListAsync();

        return orderItems;
    }
}
