using System.ComponentModel.DataAnnotations;

namespace DistrictSales.Api.Dto.ValidationAttributes;

internal class ValidGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not empty.
        if (value == null) return true;

        if (Guid.TryParse(value.ToString(), out var guid) is not true) return false;

        return guid != Guid.Empty;
    }
}
