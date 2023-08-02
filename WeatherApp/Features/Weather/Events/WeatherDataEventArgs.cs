using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather.Events;

public class WeatherDataEventArgs : EventArgs
{
    public WeatherData WeatherData { get; }

    public WeatherDataEventArgs(WeatherData weatherData)
    {
        WeatherData = weatherData;
    }
}
