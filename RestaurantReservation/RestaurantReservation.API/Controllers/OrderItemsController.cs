using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("order-items")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService) =>
        _orderItemService = orderItemService;

    [HttpPost]
    public async Task<IActionResult> CreateOrderItemAsync(OrderItemRequest orderItemRequest)
    {
        var orderItemResponse = await _orderItemService.CreateOrderItemAsync(orderItemRequest);

        var uri = $"{HttpContext.Request.Path}/{orderItemResponse.OrderItemId}";

        return Created(uri, orderItemResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrderItemAsync(int id)
    {
        await _orderItemService.DeleteOrderItemAsync(id);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrderItemAsync(int id, OrderItemRequest orderItemRequest)
    {
        var orderItemResponse = await _orderItemService.UpdateOrderItemAsync(id, orderItemRequest);

        return Ok(orderItemResponse);
    }
}
