using WeatherApp.Core.Features.Weather.Models;

namespace WeatherApp.Core.Features.Weather.Events;

public class WeatherDataEventArgs : EventArgs
{
    public WeatherData WeatherData { get; }

    public WeatherDataEventArgs(WeatherData weatherData)
    {
        WeatherData = weatherData;
    }
}
