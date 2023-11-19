namespace DistrictSales.Api.Dto;

internal static class Constants
{
    internal const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} characters long.";
    internal const string RangeErrorMessage = "The {0} field must be between {1} and {2}.";
    internal const string DateOnlyNotInFutureErrorMessage = "{0} cannot be in the future.";
    internal const string StringDanishPhoneNumberErrorMessage = "{0} must be a valid Danish phone number (8 digits without spaces).";
    internal const string InvalidGuidErrorMessage = "{0} must be a valid Guid.";
}
