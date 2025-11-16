using FutureWork.Domain.Entities;

namespace FutureWork.Domain.DTOs;

public record ProgressCreateDto(int ProfessionalId, int ModuleId, ProgressStatus Status, int Percentage);
public record ProgressUpdateDto(ProgressStatus Status, int Percentage);
public record ProgressReadDto(int Id, int ProfessionalId, int ModuleId, ProgressStatus Status, int Percentage, DateTime UpdatedAt);
