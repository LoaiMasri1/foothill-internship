using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("resturants")]
public class ResturantController : ControllerBase
{
    private readonly IResturantService _resturantService;

    public ResturantController(IResturantService resturantService) =>
        _resturantService = resturantService;

    [HttpPost]
    public async Task<IActionResult> CreateResturantAsync(ResturantRequest resturantRequest)
    {
        var resturantResponse = await _resturantService.CreateResturantAsync(resturantRequest);

        var uri = $"{HttpContext.Request.Path}/{resturantResponse.ResturantsId}";

        return Created(uri, resturantResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteResturantAsync(int id)
    {
        await _resturantService.DeleteResturantAsync(id);

        return NoContent();
    }

    [HttpGet("revenue/{resturantId:int}")]
    public IActionResult CalculateRestaurantRevenue(int resturantId)
    {
        var revenue = _resturantService.CalculateRestaurantRevenue(resturantId);

        return Ok(new { revenue });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateResturantAsync(int id, ResturantRequest resturantRequest)
    {
        var resturantResponse = await _resturantService.UpdateResturantAsync(id, resturantRequest);

        return Ok(resturantResponse);
    }
}
