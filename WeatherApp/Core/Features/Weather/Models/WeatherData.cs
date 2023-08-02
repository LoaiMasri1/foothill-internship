namespace WeatherApp.Core.Features.Weather.Models;

public record WeatherData(
    string Location,
    double Temperature,
    double Humidity);
