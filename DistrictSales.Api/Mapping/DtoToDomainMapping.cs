namespace DistrictSales.Api.Mapping;

public static class DtoToDomainMapping
{
    public static CreateSalesperson MapToCreateSalesperson(this CreateSalespersonRequestV1 salespersonResponse)
    {
        return new CreateSalesperson(
            FirstName: salespersonResponse.FirstName,
            LastName: salespersonResponse.LastName,
            BirthDate: salespersonResponse.BirthDate,
            HireDate: salespersonResponse.HireDate,
            Email: salespersonResponse.Email,
            PhoneNumber: salespersonResponse.PhoneNumber
        );
    }

    public static UpdateSalesperson MapToUpdateSalesperson(this UpdateSalespersonRequestV1 salespersonResponse, Guid id)
    {
        return new UpdateSalesperson(
            Id: id,
            FirstName: salespersonResponse.FirstName,
            LastName: salespersonResponse.LastName,
            BirthDate: salespersonResponse.BirthDate,
            HireDate: salespersonResponse.HireDate,
            Email: salespersonResponse.Email,
            PhoneNumber: salespersonResponse.PhoneNumber
        );
    }

    public static CreateDistrict MapToCreateDistrict(this CreateDistrictRequestV1 createDistrictRequest)
    {
        return new CreateDistrict(
            PrimarySalespersonId: createDistrictRequest.PrimarySalespersonId,
            Name: createDistrictRequest.Name,
            IsActive: createDistrictRequest.IsActive,
            NumberOfStores: createDistrictRequest.NumberOfStores
        );
    }

    public static UpdateDistrict MapToUpdateDistrict(this UpdateDistrictRequestV1 updateDistrictRequest, Guid id)
    {
        return new UpdateDistrict(
            Id: id,
            PrimarySalespersonId: updateDistrictRequest.PrimarySalespersonId,
            Name: updateDistrictRequest.Name,
            IsActive: updateDistrictRequest.IsActive,
            NumberOfStores: updateDistrictRequest.NumberOfStores
        );
    }
}
