namespace FutureWork.Domain.DTOs;

public record ModuleCreateDto(int LearningPathId, string Title, int WorkloadHours);
public record ModuleUpdateDto(string Title, int WorkloadHours);
public record ModuleReadDto(int Id, int LearningPathId, string Title, int WorkloadHours);
