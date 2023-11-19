namespace DistrictSales.Api.Domain.Models;

public record CreateSalesperson(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    DateOnly HireDate,
    string Email,
    string? PhoneNumber
);
