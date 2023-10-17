using Carter;

namespace JwtAuth.Features.Users;

public class UserModule : CarterModule
{
    public UserModule()
        : base("api/users") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/me", Me).WithName(nameof(Me)).WithOpenApi().RequireAuthorization();
    }

    public static IResult Me() => Results.Ok(new { Message = "Hello from Carter!" });
}
