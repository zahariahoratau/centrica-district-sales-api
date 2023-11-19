using System.Collections.Immutable;
using System.Data;
using Dapper;

namespace DistrictSales.Api.SqlServer.Repositories;

public class SalespeopleRepository : ISalespeopleRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public SalespeopleRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IImmutableList<Salesperson>> GetAllAsync(CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryGetAll);

        IEnumerable<DbSalesperson> salespeople = await connection
            .QueryAsync<DbSalesperson>(
                new CommandDefinition(
                    commandText: sql,
                    cancellationToken: cancellationToken
                )
            );

        return salespeople
            .Select(salesperson => salesperson.MapToSalesperson())
            .ToImmutableList();
    }

    public async Task<IImmutableList<Salesperson>> GetAllSecondaryByDistrictIdAsync(Guid districtId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryGetAllSecondaryByDistrictId);

        IEnumerable<DbSalesperson> salespeople = await connection
            .QueryAsync<DbSalesperson>(
                new CommandDefinition(
                    commandText: sql,
                    parameters: new { DistrictId = districtId },
                    cancellationToken: cancellationToken
                )
            );

        return salespeople
            .Select(salesperson => salesperson.MapToSalesperson())
            .ToImmutableList();
    }

    public async Task<Salesperson> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryGetById);

        DbSalesperson salesperson = await connection
            .QuerySingleAsync<DbSalesperson>(
                new CommandDefinition(
                    commandText: sql,
                    parameters: new { Id = id },
                    cancellationToken: cancellationToken
                )
            );

        return salesperson.MapToSalesperson();
    }

    public async Task<Salesperson> CreateAsync(CreateSalesperson createSalesperson, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryCreate);

        Guid id = await connection
            .QuerySingleAsync<Guid>(
                new CommandDefinition(
                    commandText: sql,
                    parameters: createSalesperson,
                    cancellationToken: cancellationToken
                )
            );

        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(UpdateSalesperson updateSalesperson, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryUpdate);

        int affectedRows = await connection
            .ExecuteAsync(
                new CommandDefinition(
                    commandText: sql,
                    parameters: updateSalesperson,
                    cancellationToken: cancellationToken
                )
            );

        if (affectedRows is 0)
            throw new ObjectNotFoundException($"Salesperson with Id: {updateSalesperson.Id} could not be found.");
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.GetConnection();

        string sql = SqlHelper.ReadSqlFile(SqlFileNames.SalespeopleRepositoryDelete);

        int affectedRows = await connection
            .ExecuteAsync(
                new CommandDefinition(
                    commandText: sql,
                    parameters: new { Id = id },
                    cancellationToken: cancellationToken
                )
            );

        if (affectedRows is 0)
            throw new ObjectNotFoundException($"Salesperson with Id: {id} could not be found.");
    }
}
