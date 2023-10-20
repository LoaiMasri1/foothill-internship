using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) => _orderService = orderService;

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderRequest">The order information.</param>
    /// <returns>The created order.</returns>
    /// <response code="201">Returns the created order.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateOrderAsync(OrderRequest orderRequest)
    {
        var orderResponse = await _orderService.CreateOrderAsync(orderRequest);

        var uri = $"{HttpContext.Request.Path}/{orderResponse.OrderId}";

        return Created(uri, orderResponse);
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="orderRequest">The updated order information.</param>
    /// <returns>The updated order.</returns>
    /// <response code="200">Returns the updated order.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut]
    [ProducesResponseType(typeof(OrderResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateOrderAsync(int id, OrderRequest orderRequest)
    {
        var orderResponse = await _orderService.UpdateOrderAsync(id, orderRequest);

        return Ok(orderResponse);
    }
}
