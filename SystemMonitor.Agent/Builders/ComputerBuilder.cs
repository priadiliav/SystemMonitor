using SystemMonitor.Agent.Collectors;
using SystemMonitor.Agent.Configs;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Builders;


public class ComputerBuilder(IEnumerable<IMetricCollector> collectors)
{
    /// <summary>
    /// The list of collectors that will be used to collect metrics.
    /// </summary>
    private readonly ComputerMetricsDto _metrics = new();

    /// <summary>
    /// It collects metrics from all collectors and returns the metrics.
    /// </summary>
    /// <returns></returns>

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