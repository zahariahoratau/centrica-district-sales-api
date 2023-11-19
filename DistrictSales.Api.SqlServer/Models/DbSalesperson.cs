namespace DistrictSales.Api.SqlServer.Models;

internal record DbSalesperson(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    DateOnly HireDate,
    string Email,
    string? PhoneNumber
);
