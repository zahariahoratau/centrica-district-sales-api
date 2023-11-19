using System.ComponentModel.DataAnnotations;
using DistrictSales.Api.Dto.ValidationAttributes;

namespace DistrictSales.Api.Dto.V1.Requests;

public class UpdateSalespersonRequestV1
{
    public UpdateSalespersonRequestV1(
        string? firstName,
        string? lastName,
        DateOnly? birthDate,
        DateOnly? hireDate,
        string? email,
        string? phoneNumber
    )
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        HireDate = hireDate;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    [StringLength(50, MinimumLength = 1, ErrorMessage = Constants.StringLengthErrorMessage)]
    public string? FirstName { get; }

    [StringLength(50, MinimumLength = 1, ErrorMessage = Constants.StringLengthErrorMessage)]
    public string? LastName { get; }

    [DateOnlyNotInFuture(ErrorMessage = Constants.DateOnlyNotInFutureErrorMessage)]
    public DateOnly? BirthDate { get; }

    public DateOnly? HireDate { get; }

    [EmailAddress]
    public string? Email { get; }

    [StringDanishPhoneNumber(ErrorMessage = Constants.StringDanishPhoneNumberErrorMessage)]
    public string? PhoneNumber { get; }
}
