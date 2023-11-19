namespace DistrictSales.Api.Domain.Models;

public record CreateDistrict(
    Guid PrimarySalespersonId,
    string Name,
    bool IsActive,
    short? NumberOfStores
);
