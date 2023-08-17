using AirportTicket.Common;
using AirportTicket.Common.Constants;
using AirportTicket.Core;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Services;

namespace AirportTicket.Features.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private static readonly IStorage _storage = Storage.GetInstance();
    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<User>> RegisterAsync(User user)
    {
        var userResult = await _userService.AddAsync(User.Create(
            user.Name,
            user.Password,
            user.Email,
            user.Role)
        );

        if (userResult.IsFailure)
        {
            return Result<User>.Failure(userResult.Error);
        }

        return Result<User>.Success(user);
    }

    public async Task<Result<User>> LoginAsync(string email, string password)
    {
        var users = await _storage.ReadAsync<User>();

        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user is null)
        {
            return Result<User>.Failure(Errors.User.UserNotFound);
        }



        return Result<User>.Success(user);
    }
}
