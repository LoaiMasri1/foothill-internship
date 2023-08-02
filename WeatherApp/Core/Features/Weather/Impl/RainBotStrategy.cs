using WeatherApp.Core.Features.Weather.Models;

namespace WeatherApp.Core.Features.Weather.Impl;

public class RainBotStrategy : WeatherBotStrategy
{
    private readonly double _humidityThreshold;
    public RainBotStrategy(
        string message,
        double humidityThreshold) : base(message)
    {
        _humidityThreshold = humidityThreshold;
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    => weatherData.Humidity > _humidityThreshold;
}
