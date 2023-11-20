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

    /// <summary>
    /// Gets all districts.
    /// </summary>
    /// <remarks>
    /// The response includes the primary salesperson and all secondary salespeople for each district.
    /// </remarks>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">Returns all districts as an array of DistrictResponseV1.</response>
    /// <response code="500">An unexpected error occurred.</response>
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

    /// <summary>
    /// Gets a district by id.
    /// </summary>
    /// <remarks>
    /// The response includes the primary salesperson and all secondary salespeople.
    /// </remarks>
    /// <param name="id">The GUID of the district to get.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">Returns the district as a DistrictResponseV1.</response>
    /// <response code="404">The district with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictResponseV1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<DistrictResponseV1> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        District district = await _districtsRepository.GetByIdAsync(id, cancellationToken);

        return district.MapToDistrictResponseV1();
    }

    /// <summary>
    /// Creates a new district.
    /// </summary>
    /// <param name="createDistrictRequest">The district to create, as a CreateDistrictRequestV1.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="201">Returns the created district as a DistrictResponseV1.</response>
    /// <response code="400">The district to create is invalid.</response>
    /// <response code="404">The salesperson with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
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

    /// <summary>
    /// Updates a district.
    /// </summary>
    /// <param name="id">The GUID of the district to update.</param>
    /// <param name="updateDistrictRequest">The district to update, as an UpdateDistrictRequestV1.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">Returns the updated district as a DistrictResponseV1.</response>
    /// <response code="400">The update district object is invalid.</response>
    /// <response code="404">The district with the specified id, or the salesperson id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictResponseV1))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task PutAsync(Guid id, UpdateDistrictRequestV1 updateDistrictRequest, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.UpdateAsync(id, updateDistrictRequest.MapToUpdateDistrict(id), cancellationToken);
    }

    /// <summary>
    /// Deletes a district.
    /// </summary>
    /// <param name="id">The GUID of the district to delete.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">The district was deleted successfully.</response>
    /// <response code="404">The district with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.DeleteAsync(id, cancellationToken);
    }

    /// <summary>
    /// Adds a secondary salesperson to a district.
    /// </summary>
    /// <param name="id">The GUID of the district to add the secondary salesperson to.</param>
    /// <param name="salespersonId">The GUID of the secondary salesperson to add.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">The secondary salesperson was added successfully.</response>
    /// <response code="400">The salesperson is already linked to the district.</response>
    /// <response code="404">The district with the specified id, or the salesperson id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost("{id:guid}/secondary-salespeople/{salespersonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task AddSecondarySalespersonAsync(Guid id, Guid salespersonId, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.AddSecondarySalespersonAsync(id, salespersonId, cancellationToken);
    }

    /// <summary>
    /// Removes a secondary salesperson from a district.
    /// </summary>
    /// <param name="id">The GUID of the district to remove the secondary salesperson from.</param>
    /// <param name="salespersonId">The GUID of the secondary salesperson to remove.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">The secondary salesperson was removed successfully.</response>
    /// <response code="404">The combination of district with the specified id and the salesperson id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpDelete("{id:guid}/secondary-salespeople/{salespersonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task RemoveSecondarySalespersonAsync(Guid id, Guid salespersonId, CancellationToken cancellationToken = default)
    {
        await _districtsRepository.RemoveSecondarySalespersonAsync(id, salespersonId, cancellationToken);
    }
}
