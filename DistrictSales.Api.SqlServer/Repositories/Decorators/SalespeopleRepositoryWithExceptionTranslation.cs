using System.Collections.Immutable;
using DistrictSales.Api.Domain.Exceptions;
using DistrictSales.Api.Domain.Models;
using DistrictSales.Api.Domain.Repositories;

namespace DistrictSales.Api.SqlServer.Repositories.Decorators;

public class SalespeopleRepositoryWithExceptionTranslation : ISalespeopleRepository
{
    private readonly ISalespeopleRepository _decoratedSalespeopleRepository;

    public SalespeopleRepositoryWithExceptionTranslation(ISalespeopleRepository decoratedSalespeopleRepository)
    {
        _decoratedSalespeopleRepository = decoratedSalespeopleRepository;
    }

    public async Task<IImmutableList<Salesperson>> GetAllAsync(CancellationToken cancellationToken) =>
        await _decoratedSalespeopleRepository.GetAllAsync(cancellationToken);

    public async Task<IImmutableList<Salesperson>> GetAllSecondaryByDistrictIdAsync(Guid districtId, CancellationToken cancellationToken) =>
        await _decoratedSalespeopleRepository.GetAllSecondaryByDistrictIdAsync(districtId, cancellationToken);

    public async Task<Salesperson> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _decoratedSalespeopleRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            throw new ObjectNotFoundException($"Salesperson with Id: {id} could not be found.");
        }
    }

    public async Task<Salesperson> CreateAsync(CreateSalesperson createSalesperson, CancellationToken cancellationToken) =>
        await _decoratedSalespeopleRepository.CreateAsync(createSalesperson, cancellationToken);

    public async Task UpdateAsync(UpdateSalesperson updateSalesperson, CancellationToken cancellationToken) =>
        await _decoratedSalespeopleRepository.UpdateAsync(updateSalesperson, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _decoratedSalespeopleRepository.DeleteAsync(id, cancellationToken);
}
