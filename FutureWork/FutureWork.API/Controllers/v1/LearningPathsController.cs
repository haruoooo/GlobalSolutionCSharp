using FutureWork.Business.Interfaces;
using FutureWork.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FutureWork.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class LearningPathsController : ControllerBase
{
    private readonly ILearningPathService _service;
    public LearningPathsController(ILearningPathService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LearningPathReadDto>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearningPathReadDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LearningPathReadDto>> Create([FromBody] LearningPathCreateDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] LearningPathUpdateDto dto, CancellationToken ct)
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
