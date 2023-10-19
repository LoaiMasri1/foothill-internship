﻿using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<CustomerResponse> RegisterAsync(CustomerRequest request);
}

public record LoginRequest(
    string Email,
    string Password
    );

public record AuthResponse(string Token);
