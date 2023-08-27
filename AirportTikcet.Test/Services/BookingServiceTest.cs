using AirportTicket.Common;
using AirportTicket.Common.Constants;
using AirportTicket.Core;
using AirportTicket.Features.Bookings.Models;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTikcet.Test.Customization;

namespace AirportTikcet.Test.Services;

public class BookingServiceTest
{
    private readonly Fixture _fixture;
    private readonly Booking _booking;
    private readonly Mock<IStorage> _storage;
    private readonly Mock<IFlightService> _flightService;
    private readonly IBookingService _bookingService;

    public BookingServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Customize(new BookingCustomization());
        _booking = _fixture.Create<Booking>();
        _storage = new Mock<IStorage>();
        _flightService = new Mock<IFlightService>();
        _bookingService = new BookingService(_flightService.Object, _storage.Object);
    }

    [Fact]
    public async Task AddAsync_ValidBooking_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>()).ReturnsAsync(new List<Booking>());

        _flightService
            .Setup(s => s.Get(It.IsAny<Func<Flight, bool>>()))
            .Returns(Result<Flight?>.Success(_booking.Flight));


        // Act
        var result = await _bookingService.AddAsync(_booking);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_booking, result.Value);
    }


    [Fact]
    public async Task AddAsync_InvalidBooking_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>()).ReturnsAsync(new List<Booking>());
        _flightService
            .Setup(s => s.Get(It.IsAny<Func<Flight, bool>>()))
            .Returns(Result<Flight?>.Success(_booking.Flight));
        _booking.DateUtc = DateTime.UtcNow.AddDays(-1);

        // Act
        var result = await _bookingService.AddAsync(_booking);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                       Errors.Booking.BookingNotValid(It.IsAny<string>()).Code,
                                  result.Error.Code
                                             );
    }

    [Fact]
    public async Task AddAsync_BookingAlreadyExists_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>()).ReturnsAsync(new List<Booking> { _booking });
        _flightService
            .Setup(s => s.Get(It.IsAny<Func<Flight, bool>>()))
            .Returns(Result<Flight?>.Success(_booking.Flight));

        // Act
        var result = await _bookingService.AddAsync(_booking);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                                  Errors.Booking.BookingAlreadyExists.Code,
                                                                   result.Error.Code);
    }

    [Fact]
    public void Get_BookingExists_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>()).ReturnsAsync(new List<Booking> { _booking });

        // Act
        var result = _bookingService.Get(b => b.Id == _booking.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_booking, result.Value);
    }

    [Fact]
    public void Get_BookingNotExists_Failure()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>()).ReturnsAsync(new List<Booking> { _booking });

        // Act
        var result = _bookingService.Get(b => b.Id == Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
                                  Errors.Booking.BookingNotFound.Code,
                                                                   result.Error.Code);
    }

    [Fact]
    public async Task Update_BookingExists_Success()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>())
                .ReturnsAsync(new List<Booking> { _booking });

        _storage.Setup(s => s.WriteAsync(It.IsAny<ICollection<Booking>>()))
                .Returns(Task.CompletedTask);

        _flightService
            .Setup(s => s.Get(It.IsAny<Func<Flight, bool>>()))
            .Returns(Result<Flight?>.Success(_booking.Flight));

        // Act
        var result = await _bookingService.Update(_booking.Id, _booking);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_booking, result.Value);
    }

    [Fact]
    public async Task Update_BookingNotExist_ShouldFail()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<Booking>())
                .ReturnsAsync(new List<Booking>());

        _storage.Setup(s => s.WriteAsync(It.IsAny<ICollection<Booking>>()))
                .Returns(Task.CompletedTask);

        _flightService
            .Setup(s => s.Get(It.IsAny<Func<Flight, bool>>()))
            .Returns(Result<Flight?>.Success(_booking.Flight));

        // Act
        var result = await _bookingService.Update(_booking.Id, _booking);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.Booking.BookingNotFound.Code,
            result.Error.Code);

    }
}
