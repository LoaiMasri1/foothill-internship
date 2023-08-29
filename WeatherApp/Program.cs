using WeatherApp.Common;
using WeatherApp.Common.Models;
using WeatherApp.Core.Configuration;
using WeatherApp.Features.Weather;
using WeatherApp.Features.Weather.Events.Handler;
using WeatherApp.Features.Weather.Factories;

IInputParser<Dictionary<string,BotConfiguration>> parser =
    new JSONInputParser<Dictionary<string, BotConfiguration>>();

ConfigurationReader configurationReader = new(parser);

var configuration = configurationReader.ReadConfiguration("appsettings.json");

IWeatherBotFactory botFactory = new WeatherBotFactory(configuration);


WeatherStation weatherStation = new();

BotActivationHandler botHandler = new(botFactory);

weatherStation.WeatherDataReceived += botHandler.Handle;

weatherStation.Start();

