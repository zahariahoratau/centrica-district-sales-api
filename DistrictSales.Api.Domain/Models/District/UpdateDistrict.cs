namespace DistrictSales.Api.Domain.Models;

public record UpdateDistrict(
    Guid Id,
    Guid? PrimarySalespersonId,
    string? Name,
    bool? IsActive,
    short? NumberOfStores
);
