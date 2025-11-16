using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FutureWork.Business.Interfaces;
using FutureWork.Data.Context;
using FutureWork.Domain.DTOs;
using FutureWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureWork.Business.Services
{
    public class ProgressService : IProgressService
    {
        private readonly AppDbContext _db;
        public ProgressService(AppDbContext db) => _db = db;

        public async Task<IReadOnlyList<ProgressReadDto>> GetByProfessionalAsync(int professionalId, CancellationToken ct)
        {
            return await _db.Progresses
                .AsNoTracking()
                .Where(p => p.ProfessionalId == professionalId)
                .Select(p => new ProgressReadDto(
                    p.Id,
                    p.ProfessionalId,
                    p.ModuleId,
                    p.Status,        
                    p.Percentage,    
                    p.UpdatedAt))
                .ToListAsync(ct);
        }

        public async Task<ProgressReadDto?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Progresses
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProgressReadDto(
                    p.Id,
                    p.ProfessionalId,
                    p.ModuleId,
                    p.Status,
                    p.Percentage,
                    p.UpdatedAt))
                .FirstOrDefaultAsync(ct);
        }

        public async Task<ProgressReadDto> CreateAsync(ProgressCreateDto dto, CancellationToken ct)
        {
            var profExists = await _db.Professionals.AnyAsync(p => p.Id == dto.ProfessionalId, ct);
            var modExists = await _db.Modules.AnyAsync(m => m.Id == dto.ModuleId, ct);
            if (!profExists || !modExists)
                throw new InvalidOperationException("Professional or Module not found.");

            var entity = new Progress
            {
                ProfessionalId = dto.ProfessionalId,
                ModuleId = dto.ModuleId,
                Status = dto.Status,     
                Percentage = dto.Percentage,  
                UpdatedAt = DateTime.UtcNow
            };

            _db.Progresses.Add(entity);
            await _db.SaveChangesAsync(ct);

            return new ProgressReadDto(
                entity.Id,
                entity.ProfessionalId,
                entity.ModuleId,
                entity.Status,
                entity.Percentage,
                entity.UpdatedAt);
        }

        public async Task<bool> UpdateAsync(int id, ProgressUpdateDto dto, CancellationToken ct)
        {
            var entity = await _db.Progresses.FindAsync([id], ct);
            if (entity is null) return false;

            entity.Status = dto.Status;      
            entity.Percentage = dto.Percentage;  
            entity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            var entity = await _db.Progresses.FindAsync([id], ct);
            if (entity is null) return false;

            _db.Progresses.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
