using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather.Impl;

public class RainBotStrategy : WeatherBotStrategy
{
    private readonly double _humidityThreshold;
    public RainBotStrategy(
        string message,
        double humidityThreshold) : base(message)
    {
        _humidityThreshold = humidityThreshold;
    }

    public override bool ShouldActivate(WeatherData weatherData)
    => weatherData.Humidity > _humidityThreshold;
}
