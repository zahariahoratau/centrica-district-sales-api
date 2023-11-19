namespace DistrictSales.Api.Domain.Models;

public record UpdateSalesperson(
    Guid Id,
    string? FirstName,
    string? LastName,
    DateOnly? BirthDate,
    DateOnly? HireDate,
    string? Email,
    string? PhoneNumber
);
