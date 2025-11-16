using FutureWork.Data.Context;
using FutureWork.Domain.DTOs;
using FutureWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FutureWork.Business.Interfaces;

public class ProfessionalService : IProfessionalService
{
    private readonly AppDbContext _db;

    public ProfessionalService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ProfessionalReadDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Professionals
            .AsNoTracking()
            .Select(p => new ProfessionalReadDto(p.Id, p.Name, p.Email))
            .ToListAsync(ct);
    }

    public async Task<ProfessionalReadDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _db.Professionals
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProfessionalReadDto(p.Id, p.Name, p.Email))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ProfessionalReadDto> CreateAsync(ProfessionalCreateDto dto, CancellationToken ct)
    {
        var exists = await _db.Professionals.AnyAsync(p => p.Email == dto.Email, ct);
        if (exists) throw new InvalidOperationException("Email already in use.");

        var entity = new Professional { Name = dto.Name, Email = dto.Email };
        _db.Professionals.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new ProfessionalReadDto(entity.Id, entity.Name, entity.Email);
    }

    public async Task<bool> UpdateAsync(int id, ProfessionalUpdateDto dto, CancellationToken ct)
    {
        var entity = await _db.Professionals.FindAsync([id], ct);
        if (entity is null) return false;

        var conflict = await _db.Professionals
            .AnyAsync(p => p.Id != id && p.Email == dto.Email, ct);
        if (conflict) throw new InvalidOperationException("Email already in use.");

        entity.Name = dto.Name;
        entity.Email = dto.Email;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Professionals.FindAsync([id], ct);
        if (entity is null) return false;

        _db.Professionals.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
