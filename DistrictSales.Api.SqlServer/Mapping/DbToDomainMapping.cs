namespace DistrictSales.Api.SqlServer.Mapping;

internal static class DbToDomainMapping
{
    public static District MapToDistrict(
        this DbDistrict dbDistrict,
        Salesperson primarySalesperson,
        IEnumerable<Salesperson> secondarySalespeople
    )
    {
        return new District(
            Id: dbDistrict.Id,
            Name: dbDistrict.Name,
            CreatedAtUtc: dbDistrict.CreatedAtUtc,
            IsActive: dbDistrict.IsActive,
            NumberOfStores: dbDistrict.NumberOfStores,
            PrimarySalesperson: primarySalesperson,
            SecondarySalespeople: secondarySalespeople
        );
    }

    public static Salesperson MapToSalesperson(this DbSalesperson dbSalesperson)
    {
        return new Salesperson(
            Id: dbSalesperson.Id,
            FirstName: dbSalesperson.FirstName,
            LastName: dbSalesperson.LastName,
            BirthDate: dbSalesperson.BirthDate,
            HireDate: dbSalesperson.HireDate,
            Email: dbSalesperson.Email,
            PhoneNumber: dbSalesperson.PhoneNumber
        );
    }
}
