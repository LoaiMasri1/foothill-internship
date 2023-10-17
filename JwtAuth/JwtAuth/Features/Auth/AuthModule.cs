using Carter;
using JwtAuth.Features.Auth.Contracts;
using JwtAuth.Features.Auth.Services;

namespace JwtAuth.Features.Auth;

public class AuthModule : CarterModule
{
    public AuthModule()
        : base("api/auth") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", Login).AllowAnonymous().WithName(nameof(Login)).WithOpenApi();
    }

    public static IResult Login(LoginRequest request, IAuthService authService)
    {
        try
        {
            var response = authService.Login(request);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: StatusCodes.Status401Unauthorized);
        }
    }
}
