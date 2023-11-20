using DistrictSales.Api.Dto.ValidationAttributes;

namespace DistrictSales.Api.Dto.UnitTests.ValidationAttributes;

public class StringDanishPhoneNumberAttributeUnitTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_WhenPhoneNumberIsValid()
    {
        // Arrange
        const string phoneNumber = "12345678";
        var attribute = new StringDanishPhoneNumberAttribute();

        // Act
        bool result = attribute.IsValid(phoneNumber);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("1234567a")]
    [InlineData("12345678a")]
    [InlineData("12345678 ")]
    [InlineData("12345")]
    public void IsValid_ShouldReturnFalse_WhenPhoneNumberIsInvalid(string phoneNumber)
    {
        // Arrange
        var attribute = new StringDanishPhoneNumberAttribute();

        // Act
        bool result = attribute.IsValid(phoneNumber);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(12345678)]
    [InlineData(12345678.0)]
    [InlineData(12345678.1)]
    [InlineData(true)]
    public void IsValid_ShouldReturnFalse_WhenParameterIsNotString(object parameter)
    {
        // Arrange
        var attribute = new StringDanishPhoneNumberAttribute();

        // Act
        bool result = attribute.IsValid(parameter);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenPhoneNumberIsNull()
    {
        // Arrange
        string? phoneNumber = null;
        var attribute = new StringDanishPhoneNumberAttribute();

        // Act
        bool result = attribute.IsValid(phoneNumber);

        // Assert
        result.Should().BeTrue();
    }
}
