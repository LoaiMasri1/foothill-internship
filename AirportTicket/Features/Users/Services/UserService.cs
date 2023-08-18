using AirportTicket.Common;
using AirportTicket.Common.Constants;
using AirportTicket.Common.Helper;
using AirportTicket.Common.Models;
using AirportTicket.Core;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Models.Enums;

namespace AirportTicket.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IStorage _storage;
    public UserService(IStorage storage)
    {
        _storage = storage;
        
    }
    private async Task<List<User>> GetUsers()
    {
        var users = await _storage.ReadAsync<User>();
        var passengers = users.Where(u => u.Role == UserRole.Passenger).ToList();
        return passengers;
    }

    public async Task<Result<User>> AddAsync(User entity)
    {
        var users = await GetUsers();

        var validationResult = ValidateUser(entity);
        if (validationResult.IsFailure)
        {
            return Result<User>.Failure(validationResult.Error);
        }

        var isExist = users.Any(p => p.Email == entity.Email);
        if (isExist)
        {
            return Result<User>.Failure(Errors.User.UserAlreadyExists);
        }
        users.Add(entity);
        await _storage.WriteAsync(users);

        return Result<User>.Success(entity);
    }

    public Result<User?> Get(Func<User, bool> predicate)
    {
        var users = GetUsers().Result;

        var user = users.FirstOrDefault(predicate);
        if (user is null)
        {
            return Result<User?>.Failure(Errors.User.UserNotFound);
        }

        return Result<User?>.Success(user);
    }

    public Result<ICollection<User>> GetAll()
    {
        var users = GetUsers().Result;
        return Result<ICollection<User>>.Success(users);
    }

    public Result<User> Update(Guid Id, User entity)
    {
        var users  = GetUsers().Result;

        var validationResult = ValidateUser(entity);
        if (validationResult.IsFailure)
        {
            return Result<User>.Failure(validationResult.Error);
        }


        var user = users.FirstOrDefault(p => p.Id == entity.Id);
        if (user == null)
        {
            return Result<User>.Failure(Errors.User.UserNotFound);
        }
        user.Name = entity.Name;
        user.Password = entity.Password;
        user.Email = entity.Email;
        user.Role = entity.Role;
        return Result<User>.Success(user);
    }

    public static List<ValidationRule> GetValidationRules()
    {
        return ValidationHelper.GetValidationRules<User>();
    }

    public Result<ICollection<User>> GetAll(Func<User, bool> predicate)
    {
        var usersInStorage = GetUsers().Result;

        var users = usersInStorage.Where(predicate).ToList();
        return Result<ICollection<User>>.Success(users);
    }

    private static Result<User> ValidateUser(User entity)
    {
        var validationResult = ValidationHelper.Validate(entity, Errors.User.UserNotValid);
        if (validationResult.IsFailure)
        {
            return Result<User>.Failure(validationResult.Error);
        }
        return Result<User>.Success(entity);
    }

}
