using AirportTicket.Common;
using AirportTikcet.Test.Helpers;

namespace AirportTicket.Test
{
    public class ResultTests
    {
        [Fact]
        public void CreateSuccess_ShouldCreateSuccessfulResult()
        {
            // Arrange

            // Act
            var result = ResultTestHelper.CreateSuccess();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

        [Fact]
        public void CreateFailure_ShouldCreateFailedResult()
        {
            // Arrange

            // Act
            var result = ResultTestHelper.CreateFailure(
                new Error("Test.Test", "Some Error")
                );

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public void CreateSuccessWithTValue_ShouldCreateSuccessfulResultWithValue()
        {
            // Arrange

            // Act
            var result = ResultTestHelper.CreateSuccess(42);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void CreateFailureWithTValue_ShouldCreateFailedResult()
        {
            // Arrange

            // Act
            var result = ResultTestHelper.CreateFailure<int>(
                new Error("Test.Test", "Some Error")
                );

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotNull(result.Error);
        }
    }
}