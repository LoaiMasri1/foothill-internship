using WeatherApp.Core.Configuration;
using WeatherApp.Features.Weather.Impl;
using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Test.TestData;

public class Data
{
    public static WeatherData WeatherData => new()
    {
        Humidity = 40,
        Temperature = 32
    };

    public static Dictionary<string, BotConfiguration> BotConfigurations => new()
    {
    {
        "RainBot", new BotConfiguration
        {
            Message = "It's raining",
            HumidityThreshold = 0.8,
            Enabled = true
        }
    },
    {
    "SnowBot", new BotConfiguration
    {
        Message = "It's snowing",
        TemperatureThreshold = 0,
        Enabled = false
    }
    },
    {
    "SunBot", new BotConfiguration
    {
        Message = "It's sunny",
        TemperatureThreshold = 0,
        Enabled = true
    }
    }
};

    public static List<WeatherBotStrategy> WeatherBotStrategies => new()
    {
        new RainBotStrategy(
                       "It's raining",
                                  70),
        new SnowBotStrategy(
                       "It's snowing",
                                  0),
        new SunBotStrategy(
                       "It's sunny",
                                  30)
    };
}
