namespace JwtAuth.Features.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users =
        new()
        {
            new()
            {
                Username = "test",
                Password = "test@123",
                Email = "test@test"
            }
        };

    public User GetUser(string username, string password)
    {
        var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);

        if (user is null)
        {
            throw new Exception("User or passaword is invalid");
        }

        return user;
    }
}
