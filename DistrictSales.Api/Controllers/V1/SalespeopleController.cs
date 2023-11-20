using System.Collections.Immutable;
using DistrictSales.Api.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace DistrictSales.Api.Controllers.V1;

[ApiController]
[Route("/api/v1/[controller]")]
public class SalespeopleController : ControllerBase
{
    private readonly ISalespeopleRepository _salespeopleRepository;

    public SalespeopleController(ISalespeopleRepository salespeopleRepository)
    {
        _salespeopleRepository = salespeopleRepository;
    }

    /// <summary>
    /// Gets all salespeople.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">Returns all salespeople as an array of SalespersonResponseV1.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalespersonResponseV1[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalespersonResponseV1[]> GetAsync(CancellationToken cancellationToken = default)
    {
        IImmutableList<Salesperson> salespeople = await _salespeopleRepository.GetAllAsync(cancellationToken);

        return salespeople
            .Select(salesperson => salesperson.MapToSalespersonResponseV1())
            .ToArray();
    }

    /// <summary>
    /// Gets a salesperson by id.
    /// </summary>
    /// <param name="id">The GUID of the salesperson to get.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">Returns the salesperson as a SalespersonResponseV1.</response>
    /// <response code="404">The salesperson with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalespersonResponseV1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalespersonResponseV1> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Salesperson salesperson = await _salespeopleRepository.GetByIdAsync(id, cancellationToken);

        return salesperson.MapToSalespersonResponseV1();
    }

    /// <summary>
    /// Creates a new salesperson.
    /// </summary>
    /// <param name="salespersonRequest">The salesperson to create, as a CreateSalespersonRequestV1.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="201">Returns the created salesperson as a SalespersonResponseV1.</response>
    /// <response code="400">The salesperson to create is invalid.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SalespersonResponseV1))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync(CreateSalespersonRequestV1 salespersonRequest, CancellationToken cancellationToken = default)
    {
        Salesperson createdSalesperson = await _salespeopleRepository.CreateAsync(salespersonRequest.MapToCreateSalesperson(), cancellationToken);

        return CreatedAtAction(
            "Get",
            new { id = createdSalesperson.Id },
            createdSalesperson.MapToSalespersonResponseV1()
        );
    }

    /// <summary>
    /// Updates a salesperson.
    /// </summary>
    /// <param name="id">The GUID of the salesperson to update.</param>
    /// <param name="salespersonRequest">The salesperson fields to update, as an UpdateSalespersonRequestV1.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">The salesperson was updated successfully.</response>
    /// <response code="400">The update salesperson object is invalid.</response>
    /// <response code="404">The salesperson with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task PutAsync(Guid id, UpdateSalespersonRequestV1 salespersonRequest, CancellationToken cancellationToken = default)
    {
        await _salespeopleRepository.UpdateAsync(salespersonRequest.MapToUpdateSalesperson(id), cancellationToken);
    }

    /// <summary>
    /// Deletes a salesperson.
    /// </summary>
    /// <param name="id">The GUID of the salesperson to delete.</param>
    /// <param name="cancellationToken">The cancellation token to be used.</param>
    /// <response code="200">The salesperson was deleted successfully.</response>
    /// <response code="404">The salesperson with the specified id was not found.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _salespeopleRepository.DeleteAsync(id, cancellationToken);
    }
}
