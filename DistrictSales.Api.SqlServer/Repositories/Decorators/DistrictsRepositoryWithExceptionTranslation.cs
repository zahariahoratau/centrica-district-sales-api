﻿using System.Collections.Immutable;
using DistrictSales.Api.Domain.Exceptions;
using DistrictSales.Api.Domain.Models;
using DistrictSales.Api.Domain.Repositories;
using DistrictSales.Api.SqlServer.Constants;
using Microsoft.Data.SqlClient;

namespace DistrictSales.Api.SqlServer.Repositories.Decorators;

public class DistrictsRepositoryWithExceptionTranslation : IDistrictsRepository
{
    private readonly IDistrictsRepository _decoratedDistrictsRepository;

    public DistrictsRepositoryWithExceptionTranslation(IDistrictsRepository decoratedDistrictsRepository)
    {
        _decoratedDistrictsRepository = decoratedDistrictsRepository;
    }

    public async Task<IImmutableList<District>> GetAllAsync(CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.GetAllAsync(cancellationToken);

    public async Task<District> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _decoratedDistrictsRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            throw new ObjectNotFoundException($"District with Id: {id} could not be found.");
        }
    }

    public async Task<District> CreateAsync(CreateDistrict createDistrict, CancellationToken cancellationToken)
    {
        try
        {
            return await _decoratedDistrictsRepository.CreateAsync(createDistrict, cancellationToken);
        }
        catch (SqlException exception)
        {
            if (exception.Number == SqlErrorNumbers.ConstraintViolation)
                throw new ObjectNotFoundException($"Salesperson with Id: {createDistrict.PrimarySalespersonId} could not be found.");

            throw;
        }
    }

    public async Task UpdateAsync(Guid id, UpdateDistrict updateDistrict, CancellationToken cancellationToken)
    {
        try
        {
            await _decoratedDistrictsRepository.UpdateAsync(id, updateDistrict, cancellationToken);
        }
        catch (SqlException exception)
        {
            if (exception.Number == SqlErrorNumbers.ConstraintViolation)
                throw new ObjectNotFoundException($"Salesperson with Id: {updateDistrict.PrimarySalespersonId} could not be found.");

            throw;
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.DeleteAsync(id, cancellationToken);

    public async Task AddSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken)
    {
        try
        {
            await _decoratedDistrictsRepository.AddSecondarySalespersonAsync(districtId, salespersonId, cancellationToken);
        }
        catch (SqlException exception)
        {
            if (exception.Number == SqlErrorNumbers.ConstraintViolation)
                throw new ObjectNotFoundException("Either the district or the salesperson could not be found.");

            if (exception.Number == SqlErrorNumbers.DuplicateKeyInsert)
                throw new DuplicateEntryException($"The salesperson with Id: {salespersonId} is already assigned as secondary to this district.");

            throw;
        }
    }

    public async Task RemoveSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken) =>
        await _decoratedDistrictsRepository.RemoveSecondarySalespersonAsync(districtId, salespersonId, cancellationToken);
}
