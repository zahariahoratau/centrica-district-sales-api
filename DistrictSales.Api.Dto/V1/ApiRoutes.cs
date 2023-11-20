namespace DistrictSales.Api.Dto.V1;

public static class ApiRoutes
{
    private const string Base = "/api/v1";

    public static class Districts
    {
        public const string Get = Base + "/districts";
        public const string GetById = Base + "/districts/{districtId}";
        public const string Post = Base + "/districts";
        public const string Put = Base + "/districts/{districtId}";
        public const string Delete = Base + "/districts/{districtId}";
        public const string AddSecondarySalesperson = Base + "/districts/{districtId}/secondary-salespeople/{salespersonId}";
        public const string RemoveSecondarySalesperson = Base + "/districts/{districtId}/secondary-salespeople/{salespersonId}";
    }

    public static class Salespeople
    {
        public const string Get = Base + "/salespeople";
        public const string GetById = Base + "/salespeople/{salespersonId}";
        public const string Post = Base + "/salespeople";
        public const string Put = Base + "/salespeople/{salespersonId}";
        public const string Delete = Base + "/salespeople/{salespersonId}";
    }
}
