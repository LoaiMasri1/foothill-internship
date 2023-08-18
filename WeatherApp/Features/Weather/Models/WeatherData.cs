namespace WeatherApp.Features.Weather.Models;
public class WeatherData
{
    public string Location { get; set; } = null!;
    public double Temperature { get; set; }
    public double Humidity { get; set; }
}
