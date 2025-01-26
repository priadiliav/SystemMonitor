using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Collectors;

public interface IMetricCollector
{
    void Collect(ComputerMetricsDto metrics);
}