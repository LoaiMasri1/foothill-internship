using JwtAuth.Features.Auth.Contracts;

namespace JwtAuth.Features.Auth.Services;

public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
}
