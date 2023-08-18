using WeatherApp.Features.Weather.Factories;
using WeatherApp.Features.Weather.Models;
using WeatherApp.Test.TestData;

namespace WeatherApp.Test
{
    public class WeatherBotFactoryTests
    {
        private readonly Mock<IWeatherBotFactory> _weatherBotFactoryMock;
        
        public WeatherBotFactoryTests()
        {
            _weatherBotFactoryMock = new Mock<IWeatherBotFactory>();

            _weatherBotFactoryMock.Setup(factory => factory.CreateBots())
                .Returns(Data.WeatherBotStrategies);
        }

        [Fact]
        public void CreateBots_ShouldCreateExpectedNumberOfBots()
        {
            // Arrange
            var sut = _weatherBotFactoryMock.Object;

            // Act
            var bots = sut.CreateBots();

            // Assert
            Assert.Equal(3, bots.Count);
        }

        [Fact]
        public void CheckIfAllEnabledActivated_ShouldActivateCorrectBots()
        {
            // Arrange
            var sut = _weatherBotFactoryMock.Object;

            var activatedBots = new List<WeatherBotStrategy>();

            // Act
            foreach (var bot in sut.CreateBots())
            {
                if (bot.ShouldActivate(Data.WeatherData))
                {
                    activatedBots.Add(bot);
                }
            }

            // Assert
            Assert.Single(activatedBots);
            Assert.Equal(
                Data.BotConfigurations["SunBot"].Message,
                activatedBots[0].Message);
            
        }
    }
}
