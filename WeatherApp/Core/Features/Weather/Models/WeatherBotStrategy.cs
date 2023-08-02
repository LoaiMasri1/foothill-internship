using WeatherApp.Core.Configuration;

namespace WeatherApp.Core.Features.Weather.Models;

public abstract class WeatherBotStrategy
{
    protected readonly BotConfiguration _config;

    protected WeatherBotStrategy(BotConfiguration config)
    {
        _config = config;
    }

    public void Activate(WeatherData weatherData)
    {
        if (ShouldActivate(weatherData))
        {
            string response = FormatBotActivationMessage(GetType().Name, _config.Message);
            Console.WriteLine(response);
        }
    }
    protected abstract bool ShouldActivate(WeatherData weatherData);

    private static string FormatBotActivationMessage(string botName,string message)
        => $@"
        {botName} Activated!
        {botName}: {message}";
}
