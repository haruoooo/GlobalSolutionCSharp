using System;

namespace FutureWork.Domain.Entities;

public class Module
{
    public int Id { get; set; }

    public int LearningPathId { get; set; }
    public LearningPath? LearningPath { get; set; }

    public string Title { get; set; } = null!;
    public int WorkloadHours { get; set; }  

    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
