using FutureWork.Business.Interfaces;
using FutureWork.Data.Context;
using FutureWork.Domain.DTOs;
using FutureWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureWork.Business.Services;

public class ModuleService : IModuleService
{
    private readonly AppDbContext _db;
    public ModuleService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ModuleReadDto>> GetByLearningPathAsync(int learningPathId, CancellationToken ct)
    {
        return await _db.Modules
            .Where(m => m.LearningPathId == learningPathId)
            .AsNoTracking()
            .Select(m => new ModuleReadDto(m.Id, m.LearningPathId, m.Title, m.WorkloadHours))
            .ToListAsync(ct);
    }

    public async Task<ModuleReadDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _db.Modules
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new ModuleReadDto(m.Id, m.LearningPathId, m.Title, m.WorkloadHours))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ModuleReadDto> CreateAsync(ModuleCreateDto dto, CancellationToken ct)
    {
        var lpExists = await _db.LearningPaths.AnyAsync(lp => lp.Id == dto.LearningPathId, ct);
        if (!lpExists) throw new InvalidOperationException("Trilha não encontrada.");

        var entity = new Module
        {
            LearningPathId = dto.LearningPathId,
            Title = dto.Title,
            WorkloadHours = dto.WorkloadHours
        };

        _db.Modules.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new ModuleReadDto(entity.Id, entity.LearningPathId, entity.Title, entity.WorkloadHours);
    }

    public async Task<bool> UpdateAsync(int id, ModuleUpdateDto dto, CancellationToken ct)
    {
        var entity = await _db.Modules.FindAsync([id], ct);
        if (entity is null) return false;

        entity.Title = dto.Title;
        entity.WorkloadHours = dto.WorkloadHours;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Modules.FindAsync([id], ct);
        if (entity is null) return false;

        _db.Modules.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
