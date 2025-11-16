namespace FutureWork.Domain.Entities;

public class Professional
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
