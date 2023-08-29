using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather.Impl;

public class SnowBotStrategy : WeatherBotStrategy
{
    private readonly double _temperatureThreshold;
    public SnowBotStrategy(string message, double temperatureThreshold) : base(message)
    {
        _temperatureThreshold = temperatureThreshold;
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    => weatherData.Temperature < _temperatureThreshold;
}
