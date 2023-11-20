using DistrictSales.Api.Dto.ValidationAttributes;

namespace DistrictSales.Api.Dto.UnitTests.ValidationAttributes;

public class DateOnlyNotInFutureAttributeUnitTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_WhenDateIsNotInFuture()
    {
        // Arrange
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);
        var attribute = new DateOnlyNotInFutureAttribute();

        // Act
        bool isValid = attribute.IsValid(date);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenDateIsInFuture()
    {
        // Arrange
        DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        var attribute = new DateOnlyNotInFutureAttribute();

        // Act
        var isValid = attribute.IsValid(date);

        // Assert
        isValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(30)]
    [InlineData(30.5)]
    [InlineData("2021-01-01")]
    public void IsValid_ShouldReturnFalse_WhenParameterIsNotDateOnly(object parameter)
    {
        // Arrange
        var attribute = new DateOnlyNotInFutureAttribute();

        // Act
        var isValid = attribute.IsValid(parameter);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenParameterIsNull()
    {
        // Arrange
        var attribute = new DateOnlyNotInFutureAttribute();

        // Act
        var isValid = attribute.IsValid(null);

        // Assert
        isValid.Should().BeTrue();
    }
}
