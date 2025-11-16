using FutureWork.Business.Interfaces;
using FutureWork.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FutureWork.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _service;
    public ModulesController(IModuleService service) => _service = service;

    [HttpGet("learningpath/{learningPathId:int}")]
    public async Task<ActionResult<IEnumerable<ModuleReadDto>>> GetByLearningPath(int learningPathId, CancellationToken ct)
        => Ok(await _service.GetByLearningPathAsync(learningPathId, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ModuleReadDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ModuleReadDto>> Create([FromBody] ModuleCreateDto dto, CancellationToken ct)
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
    public async Task<IActionResult> Update(int id, [FromBody] ModuleUpdateDto dto, CancellationToken ct)
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
