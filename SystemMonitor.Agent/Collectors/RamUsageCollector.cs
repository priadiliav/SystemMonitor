using System.Diagnostics;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Collectors;

public class RamUsageCollector : IMetricCollector
{
    private static readonly PerformanceCounter RamCounter = new("Memory", "Available MBytes");

    public void Collect(ComputerMetricsDto metrics)
    {
        try
        {
            metrics.RamUsage = RamCounter.NextValue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error collecting RAM usage: {ex.Message}");
        }
    }
}