using AirportTicket.Common;
using AirportTicket.Features.Users.Models;

namespace AirportTicket.Features.Auth;

public interface IAuthService
{
    Task<Result<User>> RegisterAsync(User user);
    Task<Result<User>> LoginAsync(string email, string password);
}
