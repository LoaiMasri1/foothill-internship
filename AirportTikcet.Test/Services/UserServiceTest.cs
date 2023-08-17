using AirportTicket.Common.Constants;
using AirportTicket.Core;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Services;
using AirportTikcet.Test.Customization;

namespace AirportTikcet.Test.Services;

public class UserServiceTest
{
    private const string PasswordErrorMessage = "Password must be at least 6 characters long,Password must contain at least one uppercase letter, one lowercase letter and one number";
    private const string EmailErrorMessage = "Invalid Email Address";
    private readonly IFixture _fixture;
    private readonly Mock<IStorage> _storage;
    private readonly User _user;
    private readonly IUserService _userService;
    private readonly List<User> _users;

    public UserServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Customize(new UserCustomization());
        _storage = new Mock<IStorage>();
        _user = _fixture.Create<User>();
        _userService = new UserService(_storage.Object);
        _users = new List<User> { _user };

        _storage.Setup(s => s.ReadAsync<User>()).ReturnsAsync(_users);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnSuccessResult()
    {
        // Arrange
        _storage.Setup(s => s.ReadAsync<User>()).ReturnsAsync(new List<User>());
        _storage.Setup(s => s.WriteAsync(It.IsAny<List<User>>())).Returns(Task.CompletedTask);

        // Act
        var result = await _userService.AddAsync(_user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_user, result.Value);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFailureResult_WhenUserPasswordNotValid()
    {
        // Arrange
        var newUser = _user;
        newUser.Password = "test";
        var users = new List<User> { newUser };
        _storage.Setup(s => s.ReadAsync<User>()).ReturnsAsync(users);

        // Act
        var result = await _userService.AddAsync(_user);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.User.UserNotValid(It.IsAny<string>()).Code,
            result.Error.Code);

        Assert.Equal(PasswordErrorMessage, result.Error.ErrorMessage);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFailureResult_WhenUserEmailNotValid()
    {
        // Arrange
        var newUser = _user;
        newUser.Email = _fixture.Create<string>();
        var users = new List<User> { newUser };
        _storage.Setup(s => s.ReadAsync<User>()).ReturnsAsync(users);

        // Act
        var result = await _userService.AddAsync(_user);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.User.UserNotValid(It.IsAny<string>()).Code,
            result.Error.Code);

        Assert.Equal(EmailErrorMessage, result.Error.ErrorMessage);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFailureResult_WhenUserAlreadyExists()
    {
        // Arrange

        // Act
        var result = await _userService.AddAsync(_user);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.User.UserAlreadyExists.Code,
                       result.Error.Code);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void Get_ShouldReturnCorrectResult(bool userExists, bool expectedResult)
    {
        // Arrange
        var users = userExists ? new List<User> { _user } : new List<User>();
        _storage.Setup(s => s.ReadAsync<User>()).ReturnsAsync(users);

        // Act
        var result = _userService.Get(u => u.Id == _user.Id);

        // Assert
        Assert.Equal(expectedResult, result.IsSuccess);
        if (expectedResult)
        {
            Assert.Equal(_user, result.Value);
        }
        else
        {
            Assert.Equal(Errors.User.UserNotFound, result.Error);
        }
    }


    [Fact]
    public void GetAll_ShouldResturnSuccessResult()
    {

        // Arrange 

        // Act
        var result = _userService.GetAll();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_users, result.Value);

    }

    [Fact]
    public void Update_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = _user;
        _storage.Setup(s => s.WriteAsync(It.IsAny<ICollection<User>>())).Returns(Task.CompletedTask);
        // Act

        var result = _userService.Update(user.Id, user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }
}
