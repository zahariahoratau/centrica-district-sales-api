using System.Collections.Immutable;
using DistrictSales.Api.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace DistrictSales.Api.Controllers.V1;

[ApiController]
[Route("/api/v1/[controller]")]
public class DistrictsController : ControllerBase
{
    private readonly IDistrictsRepository _districtsRepository;

    public DistrictsController(IDistrictsRepository districtsRepository)
    {
        _districtsRepository = districtsRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictResponseV1[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<DistrictResponseV1[]> GetAsync(CancellationToken cancellationToken = default)
    {
        IImmutableList<District> districts = await _districtsRepository.GetAllAsync(cancellationToken);

        return districts
            .Select(district => district.MapToDistrictResponseV1())
            .ToArray();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictResponseV1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<DistrictResponseV1> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        District district = await _districtsRepository.GetByIdAsync(id, cancellationToken);

        return district.MapToDistrictResponseV1();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DistrictResponseV1))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync(CreateDistrictRequestV1 createDistrictRequest, CancellationToken cancellationToken = default)
    {
        District district = await _districtsRepository.CreateAsync(createDistrictRequest.MapToCreateDistrict(), cancellationToken);

        return CreatedAtAction(
            "Get",
            new { id = district.Id },
            district.MapToDistrictResponseV1()
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictResponseV1))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task PutAsync(Guid id, UpdateDistrictRequestV1 updateDistrictRequest, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.UpdateAsync(id, updateDistrictRequest.MapToUpdateDistrict(id), cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.DeleteAsync(id, cancellationToken);
    }

    [HttpPost("{id:guid}/secondary-salespeople/{salespersonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task AddSecondarySalespersonAsync(Guid id, Guid salespersonId, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.AddSecondarySalespersonAsync(id, salespersonId, cancellationToken);
    }

    [HttpDelete("{id:guid}/secondary-salespeople/{salespersonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task RemoveSecondarySalespersonAsync(Guid id, Guid salespersonId, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.RemoveSecondarySalespersonAsync(id, salespersonId, cancellationToken);
    }
}
