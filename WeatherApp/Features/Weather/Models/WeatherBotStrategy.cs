namespace WeatherApp.Features.Weather.Models;

public abstract class WeatherBotStrategy
{
    public string Message { get; set; }
    public bool IsActivated { get; set; }

    protected WeatherBotStrategy(string message)
    {
        Message = message;
        IsActivated = false;
    }

    public void Activate(WeatherData weatherData)
    {
        if (ShouldActivate(weatherData))
        {
            IsActivated = true;
            string response = FormatBotActivationMessage(GetType().Name, Message);
            Console.WriteLine(response);
        }
        IsActivated = false;
    }
    public abstract bool ShouldActivate(WeatherData weatherData);

    private static string FormatBotActivationMessage(string botName, string message)
        => $@"
        {botName} Activated!
        {botName}: {message}";
}
