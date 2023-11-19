using System.Data;
using Dapper;

namespace DistrictSales.Api.SqlServer.TypeHandlers;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value) => parameter.Value = value.ToString();

    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);
}
