using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class OrderItemService
{
    private readonly OrderItemRepository _orderItemRepository;

    public OrderItemService(OrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
    var newOrderItem = new OrderItem
        {
            OrderId = orderItemRequest.OrderId,
            ItemId = orderItemRequest.ItemId,
            Quantity = orderItemRequest.Quantity
        };

        await _orderItemRepository.CreateOrderItemAsync(newOrderItem);
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
        var updatedOrderItem = new OrderItem
        {
            OrderId = orderItemRequest.OrderId,
            ItemId = orderItemRequest.ItemId,
            Quantity = orderItemRequest.Quantity
        };

        var orderItem = await _orderItemRepository
            .UpdateOrderItemAsync(id, updatedOrderItem);
        
            var response = new OrderItemResponse(
            orderItem.OrderItemId,
            orderItem.OrderId,
            orderItem.ItemId,
            orderItem.Quantity);

        return response;
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        await _orderItemRepository.DeleteOrderItemAsync(id);
    }

    public async Task<IEnumerable<OrderMenuItemResponse>> ListOrdersAndMenuItemsAsync(int reservationId)
    {
        var orderItems = await _orderItemRepository
            .ListOrdersAndMenuItemsAsync(reservationId);

        return orderItems;
    }

    public async Task<IEnumerable<MenuItemResponse>> ListOrderedMenuItemsAsync(int reservationId)
    {
        var orderItems = await _orderItemRepository.ListOrderedMenuItemsAsync(reservationId);
        var response = orderItems.Select(x => new MenuItemResponse(
            x.Item.ItemId,
            x.Item.ResturantId,
            x.Item.Name,
            x.Item.Description,
            x.Item.Price
            ));

        return response;
    }

}
