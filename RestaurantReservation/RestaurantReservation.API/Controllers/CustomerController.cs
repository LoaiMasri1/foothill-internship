using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService) =>
        _customerService = customerService;

    /// <summary>
    /// Deletes a customer by their ID.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteCustomerAsync(int id)
    {
        await _customerService.DeleteCustomerAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Retrieves customers with parties larger than or equal to the specified minimum party size.
    /// </summary>
    /// <param name="minPartySize">The minimum party size to retrieve customers for.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the asynchronous operation.</returns>
    /// <response code="200">Returns the customers with large parties.</response>
    [HttpGet("large-parties/{minPartySize:int}")]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), 200)]
    [Produces("application/json")]
    public IActionResult GetCustomersWithLargeParties(int minPartySize)
    {
        var customers = _customerService.GetCustomersWithLargeParties(minPartySize);

        return Ok(customers);
    }

    /// <summary>
    /// Updates a customer by their ID.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customerRequest">The customer request containing customer details.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the asynchronous operation.</returns>
    /// <response code="200">Returns the customer response if the update is successful.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CustomerResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateCustomerAsync(int id, CustomerRequest customerRequest)
    {
        var customerResponse = await _customerService.UpdateCustomerAsync(id, customerRequest);

        return Ok(customerResponse);
    }
}
