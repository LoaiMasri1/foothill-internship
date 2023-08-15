using WeatherApp.Common.Models;
using WeatherApp.Features.Weather;
using WeatherApp.Features.Weather.Models;

namespace WeatherApp.Test
{
    public class WeatherStationTests
    {
        [Fact]
        public void Start_ValidJsonInput_RaisesWeatherDataReceivedEvent()
        {
            // Arrange
            var jsonInput = "{\"Location\":\"Test Location\",\"Temperature\":25.0}";
            var fakeParser = new Mock<IInputParser<WeatherData>>();
            fakeParser.Setup(parser => parser.Parse(jsonInput)).Returns(new WeatherData { Location = "Test Location",Temperature=25.0 });

            var weatherStation = new WeatherStation(fakeParser.Object);
          
            var eventRaised = false;
            weatherStation.WeatherDataReceived += (sender, args) => eventRaised = true;

            // Act

            weatherStation.ProcessWeatherData(jsonInput);

            // Assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void Start_InvalidInput_DoesNotRaiseWeatherDataReceivedEvent()
        {
            // Arrange
            var invalidInput = "Invalid Input";

            var weatherStation = new WeatherStation();

            var eventRaised = false;
            weatherStation.WeatherDataReceived += (sender, args) => eventRaised = true;

            //Act 
            weatherStation.ProcessWeatherData(invalidInput);

            // Assert
            Assert.False(eventRaised);
        }

        [Fact]

        public void Start_ValidXmlInput_RaisesWeatherDataReceivedEvent()
        {
            // Arrange
            var xmlInput = "<WeatherData><Location>Test Location</Location><Temperature>25.0</Temperature></WeatherData>";
            var fakeParser = new Mock<IInputParser<WeatherData>>();
            fakeParser.Setup(parser => parser.Parse(xmlInput)).Returns(new WeatherData { Location = "Test Location", Temperature = 25.0 });

            var weatherStation = new WeatherStation(fakeParser.Object);

            var eventRaised = false;
            weatherStation.WeatherDataReceived += (sender, args) => eventRaised = true;

            // Act

            weatherStation.ProcessWeatherData(xmlInput);

            // Assert
            Assert.True(eventRaised);
        }

    }
}
