using System.ComponentModel.DataAnnotations;

namespace DistrictSales.Api.Dto.ValidationAttributes;

public sealed class DateOnlyNotInFutureAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not empty.
        if (value is null) return true;

        if (value.GetType() != typeof(DateOnly)) return false;

        DateOnly date = (DateOnly)value;
        return date <= DateOnly.FromDateTime(DateTime.Today);
    }
}
