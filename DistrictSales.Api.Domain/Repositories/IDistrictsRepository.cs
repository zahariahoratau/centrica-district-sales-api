using System.Collections.Immutable;
using DistrictSales.Api.Domain.Models;

namespace DistrictSales.Api.Domain.Repositories;

public interface IDistrictsRepository
{
    Task<IImmutableList<District>> GetAllAsync(CancellationToken cancellationToken);
    Task<District> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<District> CreateAsync(CreateDistrict createDistrict, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateDistrict updateDistrict, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task AddSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken);
    Task RemoveSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken);
}
