namespace SystemMonitor.Models.Entities;

public sealed class ComputerMetrics : BaseEntity
{
    public double CpuUsage { get; set; }
    public double RamUsage { get; set; }
    public double DiskUsage { get; set; }
    public long NetworkUsage { get; set; }

    // Foreign key 
    public Guid ComputerDetailsId { get; set; } = Guid.Empty;
    public ComputerDetails ComputerDetails { get; init; } = null!;
}