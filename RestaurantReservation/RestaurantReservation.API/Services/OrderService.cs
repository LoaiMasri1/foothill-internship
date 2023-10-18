﻿using AutoMapper;
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

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
    {
        var newOrder = _mapper.Map<Order>(orderRequest);
        await _orderRepository.CreateOrderAsync(newOrder);
        var response = _mapper.Map<OrderResponse>(newOrder);

        return response;
    }

    public async Task<OrderResponse> UpdateOrderAsync(int id, OrderRequest orderRequest)
    {
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
