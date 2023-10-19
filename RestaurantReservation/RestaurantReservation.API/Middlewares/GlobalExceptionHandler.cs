using RestaurantReservation.Db;
using System.Net;

namespace RestaurantReservation.API.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private static async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (ex)
        {
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new ErrorDetails(response.StatusCode, ex.Message));
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await response.WriteAsJsonAsync(new ErrorDetails(response.StatusCode, ex.Message));
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsJsonAsync(
                    new ErrorDetails(response.StatusCode, "Something Went Wrong!")
                );
                break;
        }
    }
}

public record ErrorDetails(int StatusCode, string Message);
