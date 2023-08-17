using AirportTicket.Common.Constants;
using AirportTicket.Core;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTikcet.Test.Customization;

namespace AirportTikcet.Test.Services;

public class FlightServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IStorage> _storage;
    private readonly Flight _flight;
    private readonly IFlightService _flightService;

    public FlightServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new DepartureCustomization());
        _storage = new Mock<IStorage>();
        _flight = _fixture.Create<Flight>();
        _flightService = new FlightService(_storage.Object);


        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight>());
        _storage.Setup(s => s.WriteAsync(It.IsAny<ICollection<Flight>>())).Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task AddAsync_ValidFlight_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight>());

        // Act
        var result = await _flightService.AddAsync(_flight);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_flight, result.Value);
    }

    [Fact]
    public async Task AddAsync_InvalidFlight_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight>());
        _flight.Departure.Date = DateTime.Now.AddDays(-1);

        // Act
        var result = await _flightService.AddAsync(_flight);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            Errors.Deprature.DepratureNotValid(It.IsAny<string>()).Code,
            result.Error.Code
            );
    }

    [Fact]
    public async Task AddAsync_FlightAlreadyExists_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });

        // Act
        var result = await _flightService.AddAsync(_flight);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                       Errors.Flight.FlightAlreadyExists.Code,
                                  result.Error.Code);
    }

    [Fact]
    public void GetAsync_FlightExists_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });

        // Act
        var result = _flightService.Get(f => f.FlightId == _flight.FlightId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_flight, result.Value);
    }

    [Fact]
    public void GetAsync_FlightDoesNotExist_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });

        // Act
        var result = _flightService.Get(f => f.FlightId == Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                                  Errors.Flight.FlightNotFound.Code,
                                                                   result.Error.Code);
    }

    [Fact]
    public void GetAllAsync_FlightExists_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });

        // Act
        var result = _flightService.GetAll(f => f.FlightId == _flight.FlightId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(new List<Flight> { _flight }, result.Value);
    }

    [Fact]
    public void GetAllAsync_FlightDoesNotExist_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });

        // Act
        var result = _flightService.GetAll(f => f.FlightId == Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                                             Errors.Flight.FlightNotFound.Code,
                                                                                                               result.Error.Code);
    }

    [Fact]
    public void UpdateAsync__FlightDoesNotExist_ShouldFail()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight>());

        // Act
        var result = _flightService.Update(It.IsAny<Guid>(), _flight);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            Errors.Flight.FlightNotFound.Code,
            result.Error.Code);
    }

    [Fact]
    public void UpdateAsync_FlighExist_ShouldUpdate()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Flight>()).ReturnsAsync(new List<Flight> { _flight });
        _storage.Setup(s => s.WriteAsync(It.IsAny<ICollection<Flight>>())).Returns(Task.CompletedTask);

        // Act
        var result = _flightService.Update(_flight.FlightId, _flight);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_flight, result.Value);
    }
}





