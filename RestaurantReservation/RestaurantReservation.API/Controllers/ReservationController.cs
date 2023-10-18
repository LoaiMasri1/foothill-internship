using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService) =>
        _reservationService = reservationService;

    [HttpPost]
    public async Task<IActionResult> CreateReservationAsync(ReservationRequest reservationRequest)
    {
        var reservationResponse = await _reservationService.CreateReservationAsync(
            reservationRequest
        );

        var uri = $"{HttpContext.Request.Path}/{reservationResponse.ReservationsId}";

        return Created(uri, reservationResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReservationAsync(int id)
    {
        await _reservationService.DeleteReservationAsync(id);

        return NoContent();
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetReservationsByCustomerAsync(int customerId)
    {
        var reservations = await _reservationService.GetReservationsByCustomerAsync(customerId);

        return Ok(reservations);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateReservationAsync(
        int id,
        ReservationRequest reservationRequest
    )
    {
        var reservationResponse = await _reservationService.UpdateReservationAsync(
            id,
            reservationRequest
        );

        return Ok(reservationResponse);
    }
}
