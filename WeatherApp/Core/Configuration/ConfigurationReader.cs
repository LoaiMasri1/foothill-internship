using WeatherApp.Common.Models;

namespace WeatherApp.Core.Configuration;

public class ConfigurationReader
{
    private readonly IInputParser<Dictionary<string, BotConfiguration>> _parser;
    public ConfigurationReader(IInputParser<Dictionary<string, BotConfiguration>> parser)
    => _parser = parser;

    public Dictionary<string, BotConfiguration> ReadConfiguration(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var data = _parser.Parse(json)
         ?? throw new Exception("Invalid configuration file");

        var configuration = new Dictionary<string, BotConfiguration>();
        foreach (var (key, value) in data)
        {
            configuration.Add(key, value);
        }

        return configuration;
    }
}