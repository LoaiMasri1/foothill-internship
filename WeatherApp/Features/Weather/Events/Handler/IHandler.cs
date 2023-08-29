namespace WeatherApp.Features.Weather.Events.Handler;

public interface IHandler<T>
{
    void Handle(object sender, T e);
}
