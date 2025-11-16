using FutureWork.Business.Interfaces;
using FutureWork.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FutureWork.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly IProfessionalService _service;
    public ProfessionalsController(IProfessionalService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessionalReadDto>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProfessionalReadDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProfessionalReadDto>> Create([FromBody] ProfessionalCreateDto dto, CancellationToken ct)
    {
        try
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProfessionalUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var ok = await _service.UpdateAsync(id, dto, ct);
            return ok ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
