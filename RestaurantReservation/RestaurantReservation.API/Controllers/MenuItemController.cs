using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("menu-items")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    /// <param name="menuItemRequest">The menu item request.</param>
    /// <returns>The created menu item.</returns>
    /// <response code="201">Returns the created menu item.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(MenuItemResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateMenuItemAsync(MenuItemRequest menuItemRequest)
    {
        var menuItemResponse = await _menuItemService.CreateMenuItemAsync(menuItemRequest);

        var uri = $"{HttpContext.Request.Path}/{menuItemResponse.ItemId}";

        return Created(uri, menuItemResponse);
    }

    /// <summary>
    /// Deletes a menu item by ID.
    /// </summary>
    /// <param name="id">The ID of the menu item to delete.</param>
    /// <returns>A response with no content.</returns>
    /// <response code="204">No content.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteMenuItemAsync(int id)
    {
        await _menuItemService.DeleteMenuItemAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Updates a menu item by ID.
    /// </summary>
    /// <param name="id">The ID of the menu item to update.</param>
    /// <param name="menuItemRequest">The updated menu item request.</param>
    /// <returns>The updated menu item.</returns>
    /// <response code="200">Returns the updated menu item.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(MenuItemResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateMenuItemAsync(int id, MenuItemRequest menuItemRequest)
    {
        var menuItemResponse = await _menuItemService.UpdateMenuItemAsync(id, menuItemRequest);

        return Ok(menuItemResponse);
    }
}
