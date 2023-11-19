namespace DistrictSales.Api.Dto.V1.Responses;

public class DistrictResponseV1
{
    public DistrictResponseV1(
        Guid id,
        string name,
        DateTime createdAtUtc,
        bool isActive,
        short? numberOfStores,
        SalespersonResponseV1 primarySalesperson,
        SalespersonResponseV1[] secondarySalespeople
    )
    {
        Id = id;
        Name = name;
        CreatedAtUtc = createdAtUtc;
        IsActive = isActive;
        NumberOfStores = numberOfStores;
        PrimarySalesperson = primarySalesperson;
        SecondarySalespeople = secondarySalespeople;
    }

    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAtUtc { get; }
    public bool IsActive { get; }
    public short? NumberOfStores { get; }
    public SalespersonResponseV1 PrimarySalesperson { get; }
    public SalespersonResponseV1[] SecondarySalespeople { get; }
}
