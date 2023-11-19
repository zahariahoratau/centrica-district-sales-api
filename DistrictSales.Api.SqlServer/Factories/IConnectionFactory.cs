using System.Data;

namespace DistrictSales.Api.SqlServer.Factories;

public interface IConnectionFactory
{
    IDbConnection GetConnection();
}
