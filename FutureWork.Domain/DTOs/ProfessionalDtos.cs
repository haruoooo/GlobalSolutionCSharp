namespace FutureWork.Domain.DTOs;

public record ProfessionalCreateDto(string Name, string Email);
public record ProfessionalUpdateDto(string Name, string Email);
public record ProfessionalReadDto(int Id, string Name, string Email);
