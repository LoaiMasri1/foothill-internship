using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("menu-items")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;
    private readonly IOrderItemService _orderItemService;

    public MenuItemController(IMenuItemService menuItemService, IOrderItemService orderItemService)
    {
        _menuItemService = menuItemService;
        _orderItemService = orderItemService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItemAsync(MenuItemRequest menuItemRequest)
    {
        var menuItemResponse = await _menuItemService.CreateMenuItemAsync(menuItemRequest);

        var uri = $"{HttpContext.Request.Path}/{menuItemResponse.ItemId}";

        return Created(uri, menuItemResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMenuItemAsync(int id)
    {
        await _menuItemService.DeleteMenuItemAsync(id);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMenuItemAsync(int id, MenuItemRequest menuItemRequest)
    {
        var menuItemResponse = await _menuItemService.UpdateMenuItemAsync(id, menuItemRequest);

        return Ok(menuItemResponse);
    }

    [HttpGet("reservation/{reservationId:int}")]
    public async Task<IActionResult> ListOrderedMenuItemsAsync(int reservationId)
    {
        var orderedMenuItems = await _orderItemService.ListOrderedMenuItemsAsync(reservationId);

        return Ok(orderedMenuItems);
    }
}
