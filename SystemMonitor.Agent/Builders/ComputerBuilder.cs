using SystemMonitor.Agent.Collectors;
using SystemMonitor.Agent.Configs;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Builders;

public class ComputerBuilder(IEnumerable<IMetricCollector> collectors)
{
    private readonly ComputerMetricsDto _metrics = new();

    public async Task<ComputerDetailsDto> BuildAsync()
    {
        await Task.WhenAll(collectors.Select(collector => Task.Run(() => collector.Collect(_metrics))));

        return new ComputerDetailsDto
        {
            Name = Globals.GetComputerName(),
            Metrics = _metrics
        };
    }
}