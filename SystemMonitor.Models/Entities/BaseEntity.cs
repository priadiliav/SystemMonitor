namespace SystemMonitor.Models.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatingDateTime { get; set; }
    public DateTime UpdatingDateTime { get; set; }
    public int Status { get; set; }
}
