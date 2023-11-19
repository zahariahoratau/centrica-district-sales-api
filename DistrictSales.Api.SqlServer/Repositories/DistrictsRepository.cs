using System.Collections.Immutable;
using System.Data;
using Dapper;
using DistrictSales.Api.Domain.Exceptions;
using DistrictSales.Api.Domain.Models;
using DistrictSales.Api.Domain.Repositories;
using DistrictSales.Api.SqlServer.Factories;
using DistrictSales.Api.SqlServer.Helpers;
using DistrictSales.Api.SqlServer.Mapping;
using DistrictSales.Api.SqlServer.Models;
using DistrictSales.Api.SqlServer.Sql;

namespace DistrictSales.Api.SqlServer.Repositories;

public class DistrictsRepository : IDistrictsRepository
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ISalespeopleRepository _salespeopleRepository;

    public DistrictsRepository(IConnectionFactory connectionFactory, ISalespeopleRepository salespeopleRepository)
    {
        _connectionFactory = connectionFactory;
        _salespeopleRepository = salespeopleRepository;
    }

    public async Task<IImmutableList<District>> GetAllAsync(CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryGetAll);

        IEnumerable<DbDistrict> dbDistricts = await connection
            .QueryAsync<DbDistrict>(
                new CommandDefinition(
                    commandText: sql,
                    cancellationToken: cancellationToken
                )
            );

        return dbDistricts
            .ToAsyncEnumerable()
            .SelectAwait(
                async dbDistrict => dbDistrict
                    .MapToDistrict(
                        primarySalesperson: await _salespeopleRepository.GetByIdAsync(dbDistrict.PrimarySalespersonId, cancellationToken),
                        secondarySalespeople: await _salespeopleRepository.GetAllSecondaryByDistrictIdAsync(dbDistrict.Id, cancellationToken)
                    )
            )
            .ToEnumerable()
            .ToImmutableList();
    }

    public async Task<District> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryGetById);

        DbDistrict dbDistrict = await connection
            .QuerySingleAsync<DbDistrict>(
                new CommandDefinition(
                    commandText: sql,
                    parameters: new { Id = id },
                    cancellationToken: cancellationToken
                )
            );

        return dbDistrict.MapToDistrict(
            primarySalesperson: await _salespeopleRepository.GetByIdAsync(dbDistrict.PrimarySalespersonId, cancellationToken),
            secondarySalespeople: await _salespeopleRepository.GetAllSecondaryByDistrictIdAsync(dbDistrict.Id, cancellationToken)
        );
    }

    public async Task<District> CreateAsync(CreateDistrict createDistrict, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryCreate);

        Guid id = await connection
            .QuerySingleAsync<Guid>(
                new CommandDefinition(
                    commandText: sql,
                    parameters: createDistrict,
                    cancellationToken: cancellationToken
                )
            );

        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateDistrict updateDistrict, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryUpdate);

        int affectedRows = await connection
            .ExecuteAsync(
                new CommandDefinition(
                    commandText: sql,
                    parameters: updateDistrict,
                    cancellationToken: cancellationToken
                )
            );

        if (affectedRows is 0)
            throw new ObjectNotFoundException($"District with Id: {id} could not be found.");
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryDelete);

        int affectedRows = await connection
            .ExecuteAsync(
                new CommandDefinition(
                    commandText: sql,
                    parameters: new { Id = id },
                    cancellationToken: cancellationToken
                )
            );

        if (affectedRows is 0)
            throw new ObjectNotFoundException($"District with Id: {id} could not be found.");
    }

    public async Task AddSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryAddSecondarySalesperson);

        await connection.ExecuteAsync(
            new CommandDefinition(
                commandText: sql,
                parameters: new { DistrictId = districtId, SalespersonId = salespersonId },
                cancellationToken: cancellationToken
            )
        );
    }

    public async Task RemoveSecondarySalespersonAsync(Guid districtId, Guid salespersonId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.DistrictsRepositoryRemoveSecondarySalesperson);

        int affectedRows = await connection.ExecuteAsync(
            new CommandDefinition(
                commandText: sql,
                parameters: new { DistrictId = districtId, SalespersonId = salespersonId },
                cancellationToken: cancellationToken
            )
        );

        if (affectedRows is 0)
            throw new ObjectNotFoundException("The combination of the district with the salesperson could not be found.");
    }
}
