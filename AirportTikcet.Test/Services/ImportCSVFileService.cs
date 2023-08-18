using AirportTicket.Common;
using AirportTicket.Common.Models;
using AirportTicket.Common.Services.Impl;
using AirportTicket.Common.Services.Intf;
using AirportTicket.Core.Configuration;
using AirportTicket.Features.Flights.Models;
using AirportTikcet.Test.Customization;

namespace AirportTikcet.Test.Services;

public class ImportCSVFileServiceTest
{
    private readonly IFixture _fixture;
    private readonly Flight _flight;
    private readonly Mock<IBaseService<Flight>> _flightService;
    private readonly Mock<ICSVReader> _csvReader;
    private readonly IImportFileService<Flight> _importCSVFileService;

    public ImportCSVFileServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Customize(new DepartureCustomization());

        _flight = _fixture.Create<Flight>();

        _flightService = new Mock<IBaseService<Flight>>();
        _flightService.Setup(s => s.AddAsync(It.IsAny<Flight>()))
            .ReturnsAsync(Result<Flight>.Success(_flight));

        _csvReader = new Mock<ICSVReader>();
        _csvReader.Setup(
            c => c.Read<Flight, FlightMap>(It.IsAny<string>()))
            .Returns(new List<Flight> { _flight });

        _importCSVFileService = new ImportCSVFileService<Flight, FlightMap>(_csvReader.Object);
    }

    [Fact]

    public async Task ImportAsync_ShouldReturnSuccessResult()
    {
        // Arrange
        var fileName = "test.csv";

        // Act
        var result = await _importCSVFileService.ImportAsync(fileName, _flightService.Object);

        // Assert

        _csvReader.Verify(c => c.Read<Flight, FlightMap>(It.IsAny<string>()), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal(_flight, result.Value.First());
    }


}
