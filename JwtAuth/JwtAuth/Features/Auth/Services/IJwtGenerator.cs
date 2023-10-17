using JwtAuth.Features.Users;

namespace JwtAuth.Features.Auth.Services;

public interface IJwtGenerator
{
    string Generate(User user);
}
