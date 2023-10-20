using RestaurantReservation.API.Middlewares;

namespace RestaurantReservation.API.Configurations;

public static class WebApplicationExtentions
{
    public static WebApplication Configure(this WebApplication app)
    {
        app.AddSwagger().AddMiddlewares().Run();

        return app;
    }

    private static WebApplication AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    private static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();

        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
