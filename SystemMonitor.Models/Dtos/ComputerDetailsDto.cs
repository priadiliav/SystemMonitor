namespace SystemMonitor.Models.Dtos;

public class ComputerDetailsDto
{
    public string Name { get; set; } = string.Empty;
    public ComputerMetricsDto Metrics { get; set; } = null!;
}
