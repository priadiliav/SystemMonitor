namespace SystemMonitor.Models.Dtos;

public class ComputerDetailsDto
{
    public string Name { get; set; } = string.Empty;
    public ComputerMetricsDto Metrics { get; set; } = null!;

    public override string ToString()
    {
        return $"Name: {Name}, " +
               $"Cpu: {Metrics.CpuUsage}, " +
               $"Network: {Metrics.NetworkUsage}, " +
               $"Disk: {Metrics.DiskUsage}, " +
               $"Ram: {Metrics.RamUsage} ";
    }
}
