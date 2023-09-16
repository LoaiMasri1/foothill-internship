using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderItemService
{
    private readonly RestaurantReservationDbContext _context;

    public OrderItemService(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
        var isOrderExist = await _context.Orders
            .AnyAsync(x => x.OrderId == orderItemRequest.OrderId);

        var isItemExist = await _context.MenuItems
            .AnyAsync(x => x.ItemId == orderItemRequest.ItemId);

        if (!isOrderExist || !isItemExist)
        {
            throw new NotFoundException($"Order with id {orderItemRequest.OrderId} or item with id {orderItemRequest.ItemId} does not exist");
        }

        var newOrderItem = new OrderItem
        {
            OrderId = orderItemRequest.OrderId,
            ItemId = orderItemRequest.ItemId,
            Quantity = orderItemRequest.Quantity
        };

        await _context.OrderItems.AddAsync(newOrderItem);
        await _context.SaveChangesAsync();

        var response = new OrderItemResponse(
            newOrderItem.OrderItemId,
            newOrderItem.OrderId,
            newOrderItem.ItemId,
            newOrderItem.Quantity);

        return response;
    }

    public async Task<OrderItemResponse> UpdateOrderItemAsync(
        int id, OrderItemRequest orderItemRequest)
    {
        var orderItem = await _context.OrderItems.FindAsync(id)
            ?? throw new NotFoundException($"Order item with id {id} does not exist");

        var isOrderExist = await _context.Orders
            .AnyAsync(x => x.OrderId == orderItemRequest.OrderId);

        var isItemExist = await _context.MenuItems
            .AnyAsync(x => x.ItemId == orderItemRequest.ItemId);

        if (!isOrderExist || !isItemExist)
        {
            throw new NotFoundException($"Order with id {orderItemRequest.OrderId} or item with id {orderItemRequest.ItemId} does not exist");
        }

        orderItem.OrderId = orderItemRequest.OrderId;
        orderItem.ItemId = orderItemRequest.ItemId;
        orderItem.Quantity = orderItemRequest.Quantity;

        await _context.SaveChangesAsync();

        var response = new OrderItemResponse(
            orderItem.OrderItemId,
            orderItem.OrderId,
            orderItem.ItemId,
            orderItem.Quantity);

        return response;
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id)
            ?? throw new NotFoundException($"Order item with id {id} does not exist");

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
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

}
