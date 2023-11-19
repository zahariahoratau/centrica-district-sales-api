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

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalespersonResponseV1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalespersonResponseV1> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Salesperson salesperson = await _salespeopleRepository.GetByIdAsync(id, cancellationToken);

        return salesperson.MapToSalespersonResponseV1();
    }

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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task PutAsync(Guid id, UpdateSalespersonRequestV1 salespersonRequest, CancellationToken cancellationToken = default)
    {
        await _salespeopleRepository.UpdateAsync(salespersonRequest.MapToUpdateSalesperson(id), cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _salespeopleRepository.DeleteAsync(id, cancellationToken);
    }
}
