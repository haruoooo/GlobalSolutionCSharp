using FutureWork.Domain.DTOs;

namespace FutureWork.Business.Interfaces;

public interface IProgressService
{
    Task<IReadOnlyList<ProgressReadDto>> GetByProfessionalAsync(int professionalId, CancellationToken ct);
    Task<ProgressReadDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ProgressReadDto> CreateAsync(ProgressCreateDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, ProgressUpdateDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
