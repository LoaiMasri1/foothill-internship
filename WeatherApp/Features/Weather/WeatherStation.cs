using WeatherApp.Common;
using WeatherApp.Common.Models;
using WeatherApp.Features.Weather.Events;
using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather;

public class WeatherStation
{
    public event EventHandler<WeatherDataEventArgs>? WeatherDataReceived;

    public void Start()
    {
        while (true)
        {
            Console.Write("Enter weather data (JSON or XML): ");
            var input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            IInputParser<WeatherData>? parser = GetParser(input);
            if (parser == null)
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            var weatherData = parser.Parse(input);
            Console.WriteLine(weatherData?.Location);

            if (weatherData == null)
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            OnWeatherDataReceived(weatherData);
        }
    }

    private static IInputParser<WeatherData>? GetParser(string input)
    {
        if (input.StartsWith("{"))
        {
            return new JSONInputParser<WeatherData>();
        }
        else if (input.StartsWith("<"))
        {
            return new XMLInputParser<WeatherData>();
        }
        else
        {
            return null;
        }
    }


    protected virtual void OnWeatherDataReceived(WeatherData weatherData)
    {
        WeatherDataReceived?.Invoke(this, new WeatherDataEventArgs(weatherData));
    }

}
