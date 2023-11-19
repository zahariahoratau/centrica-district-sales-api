namespace DistrictSales.Api.SqlServer.Sql;

internal static class SqlFileNames
{
    public const string DistrictsRepositoryGetAll = "DistrictsRepository/GetAll.sql";
    public const string DistrictsRepositoryGetById = "DistrictsRepository/GetById.sql";
    public const string DistrictsRepositoryCreate = "DistrictsRepository/Create.sql";
    public const string DistrictsRepositoryUpdate = "DistrictsRepository/Update.sql";
    public const string DistrictsRepositoryDelete = "DistrictsRepository/Delete.sql";
    public const string DistrictsRepositoryAddSecondarySalesperson = "DistrictsRepository/AddSecondarySalesperson.sql";
    public const string DistrictsRepositoryRemoveSecondarySalesperson = "DistrictsRepository/RemoveSecondarySalesperson.sql";

    public const string SalespeopleRepositoryGetAll = "SalespeopleRepository/GetAll.sql";
    public const string SalespeopleRepositoryGetAllSecondaryByDistrictId = "SalespeopleRepository/GetAllSecondaryByDistrictId.sql";
    public const string SalespeopleRepositoryGetById = "SalespeopleRepository/GetById.sql";
    public const string SalespeopleRepositoryCreate = "SalespeopleRepository/Create.sql";
    public const string SalespeopleRepositoryUpdate = "SalespeopleRepository/Update.sql";
    public const string SalespeopleRepositoryDelete = "SalespeopleRepository/Delete.sql";
}
