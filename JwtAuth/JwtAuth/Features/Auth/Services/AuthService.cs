using JwtAuth.Features.Auth.Contracts;
using JwtAuth.Features.Users.Repositories;

namespace JwtAuth.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public LoginResponse Login(LoginRequest request)
    {
        var user = _userRepository.GetUser(request.Username, request.Password);

        var token = _jwtGenerator.Generate(user);

        return new LoginResponse(token);
    }
}
