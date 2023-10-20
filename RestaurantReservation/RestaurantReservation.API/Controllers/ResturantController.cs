using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("resturants")]
public class ResturantController : ControllerBase
{
    private readonly IResturantService _resturantService;

    public ResturantController(IResturantService resturantService) =>
        _resturantService = resturantService;

    /// <summary>
    /// Creates a new restaurant.
    /// </summary>
    /// <param name="resturantRequest">The restaurant request.</param>
    /// <returns>The created restaurant.</returns>
    /// <response code="201">Returns the created restaurant.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ResturantResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateResturantAsync(ResturantRequest resturantRequest)
    {
        var resturantResponse = await _resturantService.CreateResturantAsync(resturantRequest);

        var uri = $"{HttpContext.Request.Path}/{resturantResponse.ResturantsId}";

        return Created(uri, resturantResponse);
    }

    /// <summary>
    /// Deletes a restaurant by ID.
    /// </summary>
    /// <param name="id">The ID of the restaurant to delete.</param>
    /// <returns>A response with no content.</returns>
    /// <response code="204">No content.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteResturantAsync(int id)
    {
        await _resturantService.DeleteResturantAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Calculates the revenue of a restaurant by ID.
    /// </summary>
    /// <param name="resturantId">The ID of the restaurant to calculate revenue for.</param>
    /// <returns>A response with the calculated revenue.</returns>
    /// <response code="200">Returns the calculated revenue.</response>
    [HttpGet("revenue/{resturantId:int}")]
    [ProducesResponseType(typeof(decimal), 200)]
    public IActionResult CalculateRestaurantRevenue(int resturantId)
    {
        var revenue = _resturantService.CalculateRestaurantRevenue(resturantId);

        return Ok(new { revenue });
    }

    /// <summary>
    /// Updates a restaurant by ID.
    /// </summary>
    /// <param name="id">The ID of the restaurant to update.</param>
    /// <param name="resturantRequest">The updated restaurant request.</param>
    /// <returns>The updated restaurant.</returns>
    /// <response code="200">Returns the updated restaurant.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ResturantResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateResturantAsync(int id, ResturantRequest resturantRequest)
    {
        var resturantResponse = await _resturantService.UpdateResturantAsync(id, resturantRequest);

        return Ok(resturantResponse);
    }
}
