using WeatherApp.Core.Configuration;
using WeatherApp.Core.Features.Weather.Models;

namespace WeatherApp.Core.Features.Weather.Impl;

public class SnowBotStrategy : WeatherBotStrategy
{
    public SnowBotStrategy(BotConfiguration config) : base(config)
    {
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    {
        throw new NotImplementedException();
    }
}
