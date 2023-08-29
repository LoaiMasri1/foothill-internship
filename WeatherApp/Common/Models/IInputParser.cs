namespace WeatherApp.Common.Models;

public interface IInputParser<T>
{
    public T? Parse(string input);
}
