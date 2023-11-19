using System.Collections.Immutable;
using DistrictSales.Api.Domain.Models;

namespace DistrictSales.Api.Domain.Repositories;

public interface ISalespeopleRepository
{
    Task<IImmutableList<Salesperson>> GetAllAsync(CancellationToken cancellationToken);
    Task<IImmutableList<Salesperson>> GetAllSecondaryByDistrictIdAsync(Guid districtId, CancellationToken cancellationToken);
    Task<Salesperson> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Salesperson> CreateAsync(CreateSalesperson createSalesperson, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateSalesperson updateSalesperson, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
