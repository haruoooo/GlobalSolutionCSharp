using FutureWork.Business.Interfaces;
using FutureWork.Data.Context;
using FutureWork.Domain.DTOs;
using FutureWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureWork.Business.Services;

public class LearningPathService : ILearningPathService
{
    private readonly AppDbContext _db;
    public LearningPathService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<LearningPathReadDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.LearningPaths
            .AsNoTracking()
            .Select(lp => new LearningPathReadDto(lp.Id, lp.Title, lp.Description, lp.Area))
            .ToListAsync(ct);
    }

    public async Task<LearningPathReadDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _db.LearningPaths
            .AsNoTracking()
            .Where(lp => lp.Id == id)
            .Select(lp => new LearningPathReadDto(lp.Id, lp.Title, lp.Description, lp.Area))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<LearningPathReadDto> CreateAsync(LearningPathCreateDto dto, CancellationToken ct)
    {
        var entity = new LearningPath { Title = dto.Title, Description = dto.Description, Area = dto.Area };
        _db.LearningPaths.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new LearningPathReadDto(entity.Id, entity.Title, entity.Description, entity.Area);
    }

    public async Task<bool> UpdateAsync(int id, LearningPathUpdateDto dto, CancellationToken ct)
    {
        var entity = await _db.LearningPaths.FindAsync([id], ct);
        if (entity is null) return false;

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Area = dto.Area;

        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.LearningPaths.FindAsync([id], ct);
        if (entity is null) return false;

        _db.LearningPaths.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
