using WeatherApp.Features.Weather.Events;
using WeatherApp.Features.Weather.Factories;
using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather.Events.Handler;

public class BotActivationHandler : IHandler<WeatherDataEventArgs>
{
    private readonly List<WeatherBotStrategy> _bots = new();
    private readonly IWeatherBotFactory _factory;

    public BotActivationHandler(IWeatherBotFactory factory)
    {
        _factory = factory;
        _bots = _factory.GetBots();
    }

    public void Handle(object? sender, WeatherDataEventArgs e)
    {

        foreach (var bot in _bots)
        {
            bot.Activate(e.WeatherData);
        }
    }
}
