using WeatherApp.Core.Configuration;
using WeatherApp.Core.Features.Weather.Models;

namespace WeatherApp.Core.Features.Weather.Impl;

public class SunBotStrategy : WeatherBotStrategy
{
    public SunBotStrategy(BotConfiguration config) : base(config)
    {
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    {
        throw new NotImplementedException();
    }
}
