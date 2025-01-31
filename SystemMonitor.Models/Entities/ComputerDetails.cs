namespace SystemMonitor.Models.Entities;

public sealed class ComputerDetails : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Foreign key
    public Guid ComputerMetricsId { get; set; } = Guid.Empty;
    public ComputerMetrics Metrics { get; init; } = null!;
}