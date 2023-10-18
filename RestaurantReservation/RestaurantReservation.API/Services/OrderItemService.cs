using AutoMapper;
using FluentValidation;
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
    private readonly IValidator<OrderItemRequest> _validator;

    public OrderItemService(
        IOrderItemRepository orderItemRepository,
        IMapper mapper,
        IValidator<OrderItemRequest> validator
    )
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
        var validationResult = await _validator.ValidateAsync(orderItemRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
        var validationResult = await _validator.ValidateAsync(orderItemRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
