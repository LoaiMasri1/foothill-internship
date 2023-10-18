using AutoMapper;
using FluentValidation;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderRequest> _validator;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper,
        IValidator<OrderRequest> validator
    )
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
    {
        var validationResult = await _validator.ValidateAsync(orderRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var newOrder = _mapper.Map<Order>(orderRequest);
        await _orderRepository.CreateOrderAsync(newOrder);
        var response = _mapper.Map<OrderResponse>(newOrder);

        return response;
    }

    public async Task<OrderResponse> UpdateOrderAsync(int id, OrderRequest orderRequest)
    {
        var validationResult = await _validator.ValidateAsync(orderRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var updatedOrder = _mapper.Map<Order>(orderRequest);

        var order = await _orderRepository.UpdateOrderAsync(id, updatedOrder);

        var response = _mapper.Map<OrderResponse>(order);

        return response;
    }

    public async Task<double> CalculateAverageOrderAmountAsync(int employeeId)
    {
        var orders = await _orderRepository.CalculateAverageOrderAmountAsync(employeeId);

        return orders;
    }
}
