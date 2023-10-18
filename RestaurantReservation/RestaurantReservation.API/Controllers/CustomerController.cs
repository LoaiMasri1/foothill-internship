using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService) =>
        _customerService = customerService;

    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        var customerResponse = await _customerService.CreateCustomerAsync(customerRequest);

        var uri = $"{HttpContext.Request.Path}/{customerResponse.CustomerId}";

        return Created(uri, customerResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCustomerAsync(int id)
    {
        await _customerService.DeleteCustomerAsync(id);

        return NoContent();
    }

    [HttpGet("large-parties/{minPartySize:int}")]
    public IActionResult GetCustomersWithLargeParties(int minPartySize)
    {
        var customers = _customerService.GetCustomersWithLargeParties(minPartySize);

        return Ok(customers);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCustomerAsync(int id, CustomerRequest customerRequest)
    {
        var customerResponse = await _customerService.UpdateCustomerAsync(id, customerRequest);

        return Ok(customerResponse);
    }
}
