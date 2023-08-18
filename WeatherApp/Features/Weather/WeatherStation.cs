using WeatherApp.Common;
using WeatherApp.Common.Models;
using WeatherApp.Features.Weather.Events;
using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Features.Weather;

public class WeatherStation
{
    private IInputParser<WeatherData>? _parser;

    public event EventHandler<WeatherDataEventArgs>? WeatherDataReceived;

    public WeatherStation(IInputParser<WeatherData>? parser = null)
    {
        _parser = parser;
    }

    public void Start()
    {
        while (true)
        {
            var input = ReadInput();
            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            ProcessWeatherData(input);
        }
    }

    private static string ReadInput()
    {
        Console.Write("Enter weather data (JSON or XML): ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public void ProcessWeatherData(string input)
    {
        _parser = GetParser(input);

        if (_parser is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        var weatherData = _parser.Parse(input);
        if (weatherData == null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        Console.WriteLine(weatherData.Location);
        OnWeatherDataReceived(weatherData);
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
            return new JSONInputParser<WeatherData>();
        }
    }


    protected virtual void OnWeatherDataReceived(WeatherData weatherData)
    {
        WeatherDataReceived?.Invoke(this, new WeatherDataEventArgs(weatherData));
    }

}
