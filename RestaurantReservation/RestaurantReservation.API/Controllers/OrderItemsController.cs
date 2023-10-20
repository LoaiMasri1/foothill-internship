using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

/// <summary>
/// Controller for managing order items.
/// </summary>
[Authorize]
[ApiController]
[Route("order-items")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService) =>
        _orderItemService = orderItemService;

    /// <summary>
    /// Creates a new order item.
    /// </summary>
    /// <param name="orderItemRequest">The order item request.</param>
    /// <returns>The created order item.</returns>
    /// <response code="201">Returns the created order item.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderItemResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
        var orderItemResponse = await _orderItemService.CreateOrderItemAsync(orderItemRequest);

        var uri = $"{HttpContext.Request.Path}/{orderItemResponse.OrderItemId}";

        return Created(uri, orderItemResponse);
    }

    /// <summary>
    /// Deletes an order item by ID.
    /// </summary>
    /// <param name="id">The ID of the order item to delete.</param>
    /// <returns>A response indicating success or failure.</returns>
    /// <response code="204">No content.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteOrderItemAsync(int id)
    {
        await _orderItemService.DeleteOrderItemAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Updates an existing order item.
    /// </summary>
    /// <param name="id">The ID of the order item to update.</param>
    /// <param name="orderItemRequest">The updated order item request.</param>
    /// <returns>The updated order item.</returns>
    /// <response code="200">Returns the updated order item.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OrderItemResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateOrderItemAsync(int id, OrderItemRequest orderItemRequest)
    {
        var orderItemResponse = await _orderItemService.UpdateOrderItemAsync(id, orderItemRequest);

        return Ok(orderItemResponse);
    }
}
