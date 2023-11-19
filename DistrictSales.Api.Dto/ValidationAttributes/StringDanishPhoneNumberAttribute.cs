using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DistrictSales.Api.Dto.ValidationAttributes;

public sealed class StringDanishPhoneNumberAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not empty.
        if (value is null) return true;

        if (value.GetType() != typeof(string)) return false;

        string phoneNumber = (string)value;
        return Regex.IsMatch(phoneNumber, @"\d{8}");
    }
}
