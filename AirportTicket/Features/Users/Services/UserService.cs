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
    private static readonly List<User> _users;

    static UserService()
    {
        _users = GetUsers().Result;
    }
    private static async Task<List<User>> GetUsers()
    {
        var users = await Storage.ReadAsync<User>();
        var passengers = users.Where(u => u.Role == UserRole.Passenger).ToList();
        return passengers;
    }

    public async Task<Result<User>> AddAsync(User entity)
    {
        var validationResult = ValidateUser(entity);
        if (validationResult.IsFailure)
        {
            return Result<User>.Failure(validationResult.Error);
        }

        var isExist = _users.Any(p => p.Email == entity.Email);
        if (isExist)
        {
            return Result<User>.Failure(Errors.User.UserAlreadyExists);
        }
        _users.Add(entity);
        await Storage.WriteAsync(_users);

        return Result<User>.Success(entity);
    }

    public Result<User?> Get(Func<User, bool> predicate)
    {
        var user = _users.FirstOrDefault(predicate);
        return Result<User?>.Success(user);
    }

    public Result<ICollection<User>> GetAll()
    {
        return Result<ICollection<User>>.Success(_users);
    }

    public Result<User> Update(Guid Id, User entity)
    {

        var validationResult = ValidateUser(entity);
        if (validationResult.IsFailure)
        {
            return Result<User>.Failure(validationResult.Error);
        }


        var user = _users.FirstOrDefault(p => p.Id == entity.Id);
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
        var users = _users.Where(predicate).ToList();
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
