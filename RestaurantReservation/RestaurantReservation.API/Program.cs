using RestaurantReservation.API.Configurations;
using RestaurantReservation.API.Configurations.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();

var app = builder.Build();

app.Configure();
