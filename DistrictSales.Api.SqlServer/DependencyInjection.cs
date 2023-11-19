using Dapper;
using DistrictSales.Api.SqlServer.Repositories;
using DistrictSales.Api.SqlServer.Repositories.Decorators;
using DistrictSales.Api.SqlServer.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistrictSales.Api.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory>(serviceProvider =>
        {
            string? connectionString = serviceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("DistrictSalesConnectionString");

            if (connectionString is null)
                throw new ArgumentNullException(connectionString, "Connection string is null.");

            return new SqlServerConnectionFactory(connectionString);
        });

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddScoped<IDistrictsRepository, DistrictsRepository>();
        services.Decorate<IDistrictsRepository, DistrictsRepositoryWithRetry>();
        services.Decorate<IDistrictsRepository, DistrictsRepositoryWithExceptionTranslation>();

        services.AddScoped<ISalespeopleRepository, SalespeopleRepository>();
        services.Decorate<ISalespeopleRepository, SalespeopleRepositoryWithExceptionTranslation>();

        return services;
    }
}
