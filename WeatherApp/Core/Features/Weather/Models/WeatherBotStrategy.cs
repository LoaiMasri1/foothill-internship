using WeatherApp.Core.Configuration;

namespace WeatherApp.Core.Features.Weather.Models;

public abstract class WeatherBotStrategy
{
    protected readonly string _message;

    protected WeatherBotStrategy(string message)
    {
        _message = message;
    }

    public void Activate(WeatherData weatherData)
    {
        if (ShouldActivate(weatherData))
        {
            string response = FormatBotActivationMessage(GetType().Name, _message);
            Console.WriteLine(response);
        }
    }
    protected abstract bool ShouldActivate(WeatherData weatherData);

    private static string FormatBotActivationMessage(string botName,string message)
        => $@"
        {botName} Activated!
        {botName}: {message}";
}
