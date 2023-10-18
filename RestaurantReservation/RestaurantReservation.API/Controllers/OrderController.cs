using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) => _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(OrderRequest orderRequest)
    {
        var orderResponse = await _orderService.CreateOrderAsync(orderRequest);

        var uri = $"{HttpContext.Request.Path}/{orderResponse.OrderId}";

        return Created(uri, orderResponse);
    }

    [HttpGet("average-amount/{employeeId:int}")]
    public async Task<IActionResult> CalculateAverageOrderAmountAsync(int employeeId)
    {
        var averageOrderAmount = await _orderService.CalculateAverageOrderAmountAsync(employeeId);

        return Ok(averageOrderAmount);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderAsync(int id, OrderRequest orderRequest)
    {
        var orderResponse = await _orderService.UpdateOrderAsync(id, orderRequest);

        return Ok(orderResponse);
    }
}
