using DistrictSales.Api.Dto.ValidationAttributes;

namespace DistrictSales.Api.Dto.UnitTests.ValidationAttributes;

public class ValidGuidAttributeUnitTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_WhenGuidIsValid()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        var attribute = new ValidGuidAttribute();

        // Act
        bool result = attribute.IsValid(guid);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenGuidIsEmpty()
    {
        // Arrange
        Guid guid = Guid.Empty;
        var attribute = new ValidGuidAttribute();

        // Act
        bool result = attribute.IsValid(guid);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(30)]
    [InlineData(30.5)]
    [InlineData(true)]
    public void IsValid_ShouldReturnFalse_WhenGuidIsNotGuid(object parameter)
    {
        // Arrange
        var attribute = new ValidGuidAttribute();

        // Act
        bool result = attribute.IsValid(parameter);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenParameterIsNull()
    {
        // Arrange
        var attribute = new ValidGuidAttribute();

        // Act
        bool result = attribute.IsValid(null);

        // Assert
        result.Should().BeTrue();
    }
}
