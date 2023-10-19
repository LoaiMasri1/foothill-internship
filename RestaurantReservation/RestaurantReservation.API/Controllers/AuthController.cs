﻿using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;

namespace RestaurantReservation.API.Controllers;

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

}
