using WeatherApp.Core.Configuration;
using WeatherApp.Features.Weather.Impl;
using WeatherApp.Features.Weather.Models;
using WeatherApp.Features.Weather.Models.Enums;

namespace WeatherApp.Features.Weather.Factories;

public class WeatherBotFactory : IWeatherBotFactory
{
    private readonly Dictionary<string, BotConfiguration> botConfigurations;

    public WeatherBotFactory(Dictionary<string, BotConfiguration> botConfigurations)
    {
        this.botConfigurations = botConfigurations;
    }
    public List<WeatherBotStrategy> CreateBots()
    {
        var bots = new List<WeatherBotStrategy>();

        foreach (var (key, conf) in botConfigurations)
        {
            var type = Enum.Parse<WeatherBotType>(key);

            switch (type)
            {
                case WeatherBotType.RainBot:
                    bots.Add(new RainBotStrategy(
                        conf.Message, conf.HumidityThreshold));
                    break;
                case WeatherBotType.SnowBot:
                    bots.Add(new SnowBotStrategy(
                        conf.Message, conf.TemperatureThreshold));
                    break;
                case WeatherBotType.SunBot:
                    bots.Add(new SunBotStrategy(
                        conf.Message, conf.TemperatureThreshold));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return bots;
    }
}
