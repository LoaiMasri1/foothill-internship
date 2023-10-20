using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IOrderItemService _orderItemService;

    public ReservationController(
        IReservationService reservationService,
        IOrderItemService orderItemService
    )
    {
        _reservationService = reservationService;
        _orderItemService = orderItemService;
    }

    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="reservationRequest">The reservation request.</param>
    /// <returns>The created reservation.</returns>
    /// <response code="201">Returns the created reservation.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReservationResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateReservationAsync(ReservationRequest reservationRequest)
    {
        var reservationResponse = await _reservationService.CreateReservationAsync(
            reservationRequest
        );

        var uri = $"{HttpContext.Request.Path}/{reservationResponse.ReservationsId}";

        return Created(uri, reservationResponse);
    }

    /// <summary>
    /// Deletes a reservation by ID.
    /// </summary>
    /// <param name="id">The ID of the reservation to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">No content.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteReservationAsync(int id)
    {
        await _reservationService.DeleteReservationAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Gets all reservations for a customer by customer ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The reservations for the customer.</returns>
    /// <response code="200">Returns the reservations for the customer.</response>
    [HttpGet("customer/{customerId:int}")]
    [ProducesResponseType(typeof(IEnumerable<ReservationResponse>), 200)]
    public async Task<IActionResult> GetReservationsByCustomerAsync(int customerId)
    {
        var reservations = await _reservationService.GetReservationsByCustomerAsync(customerId);

        return Ok(reservations);
    }

    /// <summary>
    /// Updates a reservation by ID.
    /// </summary>
    /// <param name="id">The ID of the reservation to update.</param>
    /// <param name="reservationRequest">The reservation request.</param>
    /// <returns>The updated reservation.</returns>
    /// <response code="200" >Returns the updated reservation.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ReservationResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
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

    /// <summary>
    /// Lists all ordered menu items for a given reservation ID.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation.</param>
    /// <returns>A list of ordered menu items.</returns>
    /// <response code="200">Returns the list of ordered menu items.</response>
    [HttpGet("{reservationId:int}/menu-items")]
    [ProducesResponseType(typeof(IEnumerable<MenuItemResponse>), 200)]
    public async Task<IActionResult> ListOrderedMenuItemsAsync(int reservationId)
    {
        var orderedMenuItems = await _orderItemService.ListOrderedMenuItemsAsync(reservationId);

        return Ok(orderedMenuItems);
    }

    /// <summary>
    /// Lists all orders for a given reservation ID.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation.</param>
    /// <returns>A list of orders.</returns>
    /// <response code="200">Returns the list of orders.</response>
    [HttpGet("{reservationId:int}/orders")]
    [ProducesResponseType(typeof(IEnumerable<OrderMenuItemResponse>), 200)]
    public async Task<IActionResult> ListOrdersAndMenuItemsAsync(int reservationId)
    {
        var orders = await _orderItemService.ListOrdersAndMenuItemsAsync(reservationId);

        return Ok(orders);
    }
}
