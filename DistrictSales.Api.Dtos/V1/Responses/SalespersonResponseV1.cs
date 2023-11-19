namespace DistrictSales.Api.Dto.V1.Responses;

public class SalespersonResponseV1
{
    public SalespersonResponseV1(
        Guid id,
        string firstName,
        string lastName,
        DateOnly birthDate,
        DateOnly hireDate,
        string email,
        string? phoneNumber
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        HireDate = hireDate;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly BirthDate { get; }
    public DateOnly HireDate { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
}
