using JwtAuth.Features.Users;
using JwtAuth.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Features.Auth.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _jwtOptions;
    private const int expiresDay = 7;

    public JwtGenerator(IOptions<JwtOptions> jwtOptions) => _jwtOptions = jwtOptions.Value;

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var signingredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _jwtOptions.ValidIssuer,
            _jwtOptions.ValidAudience,
            claims,
            null,
            DateTime.UtcNow.AddDays(expiresDay),
            signingredentials
        );

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
