namespace DistrictSales.Api.SqlServer.Models;

internal record DbDistrict(
    Guid Id,
    Guid PrimarySalespersonId,
    string Name,
    DateTime CreatedAtUtc,
    bool IsActive,
    short? NumberOfStores
);
