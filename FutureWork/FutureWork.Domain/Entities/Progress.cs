namespace FutureWork.Domain.Entities;

public enum ProgressStatus { NotStarted = 0, InProgress = 1, Completed = 2 }

public class Progress
{
    public int Id { get; set; }

    public int ProfessionalId { get; set; }
    public Professional? Professional { get; set; }

    public int ModuleId { get; set; }
    public Module? Module { get; set; }

    public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;
    public int Percentage { get; set; } = 0;          
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
