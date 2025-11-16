using FutureWork.Domain.DTOs;

namespace FutureWork.Business.Interfaces;

public interface IProfessionalService
{
    Task<IReadOnlyList<ProfessionalReadDto>> GetAllAsync(CancellationToken ct);
    Task<ProfessionalReadDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ProfessionalReadDto> CreateAsync(ProfessionalCreateDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, ProfessionalUpdateDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
