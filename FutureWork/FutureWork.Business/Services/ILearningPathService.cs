using FutureWork.Domain.DTOs;

namespace FutureWork.Business.Interfaces;

public interface ILearningPathService
{
    Task<IReadOnlyList<LearningPathReadDto>> GetAllAsync(CancellationToken ct);
    Task<LearningPathReadDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<LearningPathReadDto> CreateAsync(LearningPathCreateDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, LearningPathUpdateDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
