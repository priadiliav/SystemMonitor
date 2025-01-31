namespace SystemMonitor.Models.Dtos;

public class ComputerMetricsDto
{
    public double CpuUsage { get; set; }
    public double RamUsage { get; set; }
    public double DiskUsage { get; set; }
    public long NetworkUsage { get; set; }
}