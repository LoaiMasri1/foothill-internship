using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather.Factories;

public interface IWeatherBotFactory
{
    List<WeatherBotStrategy> CreateBots();

    List<WeatherBotStrategy> GetBots() =>
        CreateBots();
}
