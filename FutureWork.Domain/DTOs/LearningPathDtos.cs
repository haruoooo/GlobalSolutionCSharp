namespace FutureWork.Domain.DTOs;

public record LearningPathCreateDto(string Title, string? Description, string? Area);
public record LearningPathUpdateDto(string Title, string? Description, string? Area);
public record LearningPathReadDto(int Id, string Title, string? Description, string? Area);
