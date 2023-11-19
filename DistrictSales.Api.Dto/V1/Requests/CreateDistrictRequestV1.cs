using System.ComponentModel.DataAnnotations;
using DistrictSales.Api.Dto.ValidationAttributes;

namespace DistrictSales.Api.Dto.V1.Requests;

public class CreateDistrictRequestV1
{
    public CreateDistrictRequestV1(
        Guid primarySalespersonId,
        string name,
        bool isActive,
        short? numberOfStores
    )
    {
        PrimarySalespersonId = primarySalespersonId;
        Name = name;
        IsActive = isActive;
        NumberOfStores = numberOfStores;
    }

    [ValidGuid(ErrorMessage = Constants.InvalidGuidErrorMessage)]
    public Guid PrimarySalespersonId { get; }

    [StringLength(50, MinimumLength = 1, ErrorMessage = Constants.StringLengthErrorMessage)]
    public string Name { get; }

    public bool IsActive { get; }

    [Range(1, 32000, ErrorMessage = Constants.RangeErrorMessage)]
    public short? NumberOfStores { get; }
}
