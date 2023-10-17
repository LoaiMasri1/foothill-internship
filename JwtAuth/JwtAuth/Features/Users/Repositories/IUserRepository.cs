namespace JwtAuth.Features.Users.Repositories;

public interface IUserRepository
{
    User GetUser(string username, string password);
}
