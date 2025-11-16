using FutureWork.Business.Interfaces;
using FutureWork.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FutureWork.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly IProgressService _service;
    public ProgressController(IProgressService service) => _service = service;

    [HttpGet("professional/{professionalId:int}")]
    public async Task<ActionResult<IEnumerable<ProgressReadDto>>> GetByProfessional(int professionalId, CancellationToken ct)
        => Ok(await _service.GetByProfessionalAsync(professionalId, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProgressReadDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProgressReadDto>> Create([FromBody] ProgressCreateDto dto, CancellationToken ct)
    {
        try
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProgressUpdateDto dto, CancellationToken ct)
    {
        var ok = await _service.UpdateAsync(id, dto, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
