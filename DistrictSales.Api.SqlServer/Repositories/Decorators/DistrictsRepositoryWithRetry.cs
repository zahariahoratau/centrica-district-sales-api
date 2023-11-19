using System.Collections.Immutable;
using DistrictSales.Api.Domain.Models;
using DistrictSales.Api.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace DistrictSales.Api.SqlServer.Repositories.Decorators;

/// <summary>
/// Decorator for <see cref="IDistrictsRepository"/> that retries failed operations for the database calls which are most likely to fail.
/// </summary>
public class DistrictsRepositoryWithRetry : IDistrictsRepository
{
    private readonly IDistrictsRepository _decoratedDistrictsRepository;
    private readonly ILogger<DistrictsRepositoryWithRetry> _logger;

    private readonly AsyncRetryPolicy _retryPolicy;

    public DistrictsRepositoryWithRetry(IDistrictsRepository decoratedDistrictsRepository, ILogger<DistrictsRepositoryWithRetry> logger)
    {
        _decoratedDistrictsRepository = decoratedDistrictsRepository;
        _logger = logger;

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3)
            );
    }

    public async Task<IImmutableList<District>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _retryPolicy.ExecuteAsync(async ct => await _decoratedDistrictsRepository.GetAllAsync(ct), cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogWarning("Could not get all districts from the database");
            throw;
        }
    }

    public async Task<District> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _retryPolicy.ExecuteAsync(async ct => await _decoratedDistrictsRepository.GetByIdAsync(id, ct), cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogWarning("Could not get district with id {Id} from the database", id);
            throw;
        }
    }

    public async Task<District> CreateAsync(CreateDistrict createDistrict, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.CreateAsync(createDistrict, cancellationToken);

    public async Task UpdateAsync(Guid id, UpdateDistrict updateDistrict, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.UpdateAsync(id, updateDistrict, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.DeleteAsync(id, cancellationToken);

    public async Task AddSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.AddSecondarySalespersonAsync(districtId, salespersonId, cancellationToken);

    public async Task RemoveSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.RemoveSecondarySalespersonAsync(districtId, salespersonId, cancellationToken);
}
