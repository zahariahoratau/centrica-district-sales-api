using DistrictSales.Api.Dto.V1;
using DistrictSales.Api.Dto.V1.Requests;
using DistrictSales.Api.Dto.V1.Responses;
using Refit;

namespace DistrictSales.Api.Sdk;

public interface IDistrictSalesApi
{
    [Get(ApiRoutes.Districts.Get)]
    Task<ApiResponse<DistrictResponseV1[]>> GetDistrictsAsync();

    [Get(ApiRoutes.Districts.GetById)]
    Task<ApiResponse<DistrictResponseV1>> GetDistrictAsync(Guid districtId);

    [Post(ApiRoutes.Districts.Post)]
    Task<ApiResponse<DistrictResponseV1>> CreateDistrictAsync([Body] CreateDistrictRequestV1 createDistrictRequest);

    [Put(ApiRoutes.Districts.Put)]
    Task<IApiResponse> UpdateDistrictAsync(Guid districtId, [Body] UpdateDistrictRequestV1 updateDistrictRequest);

    [Delete(ApiRoutes.Districts.Delete)]
    Task<IApiResponse> DeleteDistrictAsync(Guid districtId);

    [Post(ApiRoutes.Districts.AddSecondarySalesperson)]
    Task<IApiResponse> AddSecondarySalespersonAsync(Guid districtId, Guid salespersonId);

    [Delete(ApiRoutes.Districts.RemoveSecondarySalesperson)]
    Task<IApiResponse> RemoveSecondarySalespersonAsync(Guid districtId, Guid salespersonId);


    [Get(ApiRoutes.Salespeople.Get)]
    Task<ApiResponse<SalespersonResponseV1[]>> GetSalespeopleAsync();

    [Get(ApiRoutes.Salespeople.GetById)]
    Task<ApiResponse<SalespersonResponseV1>> GetSalespersonAsync(Guid salespersonId);

    [Post(ApiRoutes.Salespeople.Post)]
    Task<ApiResponse<SalespersonResponseV1>> CreateSalespersonAsync([Body] CreateSalespersonRequestV1 createSalespersonRequest);

    [Put(ApiRoutes.Salespeople.Put)]
    Task<IApiResponse> UpdateSalespersonAsync(Guid salespersonId, [Body] UpdateSalespersonRequestV1 updateSalespersonRequest);

    [Delete(ApiRoutes.Salespeople.Delete)]
    Task<IApiResponse> DeleteSalespersonAsync(Guid salespersonId);
}
