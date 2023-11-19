using System.Reflection;

namespace DistrictSales.Api.SqlServer.Helpers;

public static class SqlHelper
{
    public static string ReadSqlFile(string fileName)
    {
        string? executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (executingAssemblyPath is null)
            throw new InvalidOperationException("Could not get the executing assembly path.");

        string filePath = Path.Combine(executingAssemblyPath, "Sql", fileName);

        return File.ReadAllText(filePath);
    }
}
