namespace DistrictSales.Api.Mapping;

internal static class DomainToDtoMapping
{
    public static DistrictResponseV1 MapToDistrictResponseV1(this District district)
    {
        return new DistrictResponseV1(
            id: district.Id,
            name: district.Name,
            createdAtUtc: district.CreatedAtUtc,
            isActive: district.IsActive,
            numberOfStores: district.NumberOfStores,
            primarySalesperson: district.PrimarySalesperson.MapToSalespersonResponseV1(),
            secondarySalespeople: district.SecondarySalespeople.Select(MapToSalespersonResponseV1).ToArray()
        );
    }

    public static SalespersonResponseV1 MapToSalespersonResponseV1(this Salesperson salesperson)
    {
        return new SalespersonResponseV1(
            id: salesperson.Id,
            firstName: salesperson.FirstName,
            lastName: salesperson.LastName,
            birthDate: salesperson.BirthDate,
            hireDate: salesperson.HireDate,
            email: salesperson.Email,
            phoneNumber: salesperson.PhoneNumber
        );
    }
}
