using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Configurations.Options;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Db.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.API.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private const int expiresDay = 7;

    public JwtProvider(IOptions<JwtOptions> jwtOptions) => _jwtOptions = jwtOptions.Value;

    public string Generate(Customer customer)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, customer.CustomerId.ToString()),
            new(JwtRegisteredClaimNames.Email, customer.Email)};

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
