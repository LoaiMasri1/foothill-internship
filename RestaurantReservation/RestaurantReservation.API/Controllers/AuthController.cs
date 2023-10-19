using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        var customerResponse = await _authService.RegisterAsync(customerRequest);

        var uri = $"{HttpContext.Request.Path}/{customerResponse.CustomerId}";

        return Created(uri, customerResponse);
    }
}
