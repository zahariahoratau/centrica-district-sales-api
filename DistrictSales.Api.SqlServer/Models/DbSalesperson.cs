namespace DistrictSales.Api.SqlServer.Models;

public record DbSalesperson(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    DateOnly HireDate,
    string Email,
    string? PhoneNumber
);
