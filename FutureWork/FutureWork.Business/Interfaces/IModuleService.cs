using FutureWork.Domain.DTOs;

namespace FutureWork.Business.Interfaces;

public interface IModuleService
{
    Task<IReadOnlyList<ModuleReadDto>> GetByLearningPathAsync(int learningPathId, CancellationToken ct);
    Task<ModuleReadDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ModuleReadDto> CreateAsync(ModuleCreateDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, ModuleUpdateDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
