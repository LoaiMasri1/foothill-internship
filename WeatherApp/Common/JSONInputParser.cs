using System.Text.Json;
using WeatherApp.Common.Models;

namespace WeatherApp.Common;

public class JSONInputParser<T> : IInputParser<T>
{
    public T? Parse(string input)
    {
        try
        {
            var data = JsonSerializer.Deserialize<T>(input);
            return data;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error while parsing JSON: {ex.Message}");
            return default;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine($"Something Went Wrong");
            return default;
        }
    }
}

