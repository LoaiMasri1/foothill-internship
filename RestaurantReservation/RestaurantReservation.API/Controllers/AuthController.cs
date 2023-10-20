using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login request containing user credentials.</param>
    /// <returns>Returns an IActionResult representing the result of the login operation.</returns>
    /// <response code="200">Returns the login response if the login is successful.</response>
    /// <response code="401">If the login attempt is not successful (e.g., invalid credentials), returns a Unauthorized response.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 401)]
    [Produces("application/json")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    /// <summary>
    /// Registers a new customer.
    /// </summary>
    /// <param name="customerRequest">The customer request containing customer details.</param>
    /// <returns>Returns an IActionResult representing the result of the registration operation.</returns>
    /// <response code="201">Returns the customer response if the registration is successful.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(CustomerResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        var customerResponse = await _authService.RegisterAsync(customerRequest);

        var uri = $"{HttpContext.Request.Path}/{customerResponse.CustomerId}";

        return Created(uri, customerResponse);
    }
}
