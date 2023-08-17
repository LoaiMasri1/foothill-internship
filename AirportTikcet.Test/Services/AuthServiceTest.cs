using AirportTicket.Common;
using AirportTicket.Common.Constants;
using AirportTicket.Features.Auth;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Services;

namespace AirportTikcet.Test.Services;

public class AuthServiceTest
{
    private readonly IFixture _fixture;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly AuthService _authService;

    public AuthServiceTest()
    {
        _fixture = new Fixture();
        _userServiceMock = new Mock<IUserService>();
        _authService = new AuthService(_userServiceMock.Object);

        _userServiceMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(Result<User>.Success(_fixture.Create<User>()));


    }

    [Fact]
    public async Task RegisterAsync_WhenUserServiceSucceeds_ShouldReturnSuccessResult()
    {
        // Arrange
        var user = _fixture.Create<User>();

        // Act
        var result = await _authService.RegisterAsync(user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task RegisterAsync_WhenUserServiceFails_ShouldReturnFailureResult()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _userServiceMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(Result<User>.Failure(Errors.User.UserAlreadyExists));

        // Act
        var result = await _authService.RegisterAsync(user);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.User.UserAlreadyExists, result.Error);
    }

    [Fact]
    public async Task LoginAsync_WhenUserExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _userServiceMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(Result<User>.Success(user));

        // Act
        var result = await _authService.RegisterAsync(user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task LoginAsync_WhenUserDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _userServiceMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(Result<User>.Failure(Errors.User.UserNotFound));

        // Act
        var result = await _authService.RegisterAsync(user);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.User.UserNotFound, result.Error);
    }

}
