using System.Reflection;
using Serilog;

namespace DistrictSales.Api.SqlServer.Helpers;

public static class SqlHelper
{
    public static string ReadSqlFile(string fileName)
    {
        try
        {
            string? executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (executingAssemblyPath is null)
            {
                Log.Warning("Could not get the executing assembly path when reading the SQL file {FileName}", fileName);
                throw new InvalidOperationException("Could not get the executing assembly path.");
            }

            string filePath = Path.Combine(executingAssemblyPath, "Sql", fileName);

            return File.ReadAllText(filePath);
        }
        catch (Exception exception)
        {
            Log.Warning("Could not read the SQL file: {FileName}", fileName);
            throw new InvalidOperationException("Could not read SQL files", exception);
        }
    }
}
