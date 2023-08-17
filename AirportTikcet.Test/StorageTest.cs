using AirportTicket.Core;

namespace AirportTicket.Test;

public class StorageTests
{
    [Fact]
    public void GetInstance_ShouldReturnStorageInstance()
    {
        // Arrange
        var storage = Storage.GetInstance();

        // Act
        var result = storage;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Storage>(result);
    }

    [Fact]
    public void GetInstance_ShouldReturnSameStorageInstance()
    {
        // Arrange
        var storage1 = Storage.GetInstance();
        var storage2 = Storage.GetInstance();

        // Act
        var result = storage1 == storage2;

        // Assert
        Assert.True(result);
    }
}