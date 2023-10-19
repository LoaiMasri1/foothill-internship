namespace RestaurantReservation.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
}

public record LoginRequest(
    string Email,
    string Password
    );

public record AuthResponse(string Token);
