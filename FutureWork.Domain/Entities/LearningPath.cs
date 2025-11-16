using System.Reflection;

namespace FutureWork.Domain.Entities;

public class LearningPath
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Area { get; set; }

    public ICollection<Module> Modules { get; set; } = new List<Module>();
}
