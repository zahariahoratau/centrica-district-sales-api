namespace DistrictSales.Api.Domain.Models;

public record District(
    Guid Id,
    string Name,
    DateTime CreatedAtUtc,
    bool IsActive,
    short? NumberOfStores,
    Salesperson PrimarySalesperson,
    IEnumerable<Salesperson> SecondarySalespeople
);
