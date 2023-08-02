using WeatherApp.Common;

namespace WeatherApp.Core.Configuration;

public class ConfigurationReader
{
    private readonly JSONInputParser<Dictionary<string, BotConfiguration>> _parser;
    public ConfigurationReader(JSONInputParser<Dictionary<string, BotConfiguration>> parser)
    {
        _parser = parser;
    }

    public Dictionary<string, BotConfiguration> ReadConfiguration(string filePath)
    {
        var json = File.ReadAllText(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            filePath));
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