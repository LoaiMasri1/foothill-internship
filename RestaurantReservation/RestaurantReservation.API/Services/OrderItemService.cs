using AutoMapper;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(
        IOrderItemRepository orderItemRepository,
        IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
       var newOrderItem = _mapper.Map<OrderItem>(orderItemRequest);

        await _orderItemRepository.CreateOrderItemAsync(newOrderItem);
        var response = _mapper.Map<OrderItemResponse>(newOrderItem);

        return response;
    }

    public async Task<OrderItemResponse> UpdateOrderItemAsync(
        int id,
        OrderItemRequest orderItemRequest
    )
    {
        var updatedOrderItem = _mapper.Map<OrderItem>(orderItemRequest);

        var orderItem = await _orderItemRepository.UpdateOrderItemAsync(id, updatedOrderItem);

        var response = _mapper.Map<OrderItemResponse>(orderItem);

        return response;
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        await _orderItemRepository.DeleteOrderItemAsync(id);
    }

    public async Task<IEnumerable<OrderMenuItemResponse>> ListOrdersAndMenuItemsAsync(
        int reservationId
    )
    {
        var orderItems = await _orderItemRepository.ListOrdersAndMenuItemsAsync(reservationId);

        return orderItems;
    }

    public async Task<IEnumerable<MenuItemResponse>> ListOrderedMenuItemsAsync(int reservationId)
    {
        var orderItems = await _orderItemRepository.ListOrderedMenuItemsAsync(reservationId);

        var response = orderItems.Select(
            x =>
                new MenuItemResponse(
                    x.ItemId,
                    x.Item.ResturantId,
                    x.Item.Name,
                    x.Item.Description,
                    x.Item.Price
                )
        );

        return response;
    }
}
