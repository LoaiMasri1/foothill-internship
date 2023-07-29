using AirportTicket.Common;
using AirportTicket.Features.Users.Models;

namespace AirportTicket.Features.Users.Services;

public class UserService : IUserService
{
    public Task<Result<User>> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Result<User?> Get(Func<User, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Result<ICollection<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Result<ICollection<User>> GetAll(Func<User, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Result<User> Update(Guid id, User entity)
    {
        throw new NotImplementedException();
    }
}
