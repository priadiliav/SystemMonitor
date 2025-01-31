using System.Diagnostics;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Collectors;

public class CpuUsageCollector : IMetricCollector
{
    private static readonly PerformanceCounter CpuCounter = new("Processor", "% Processor Time", "_Total");

    public void Collect(ComputerMetricsDto metrics)
    {
        try
        {
            metrics.CpuUsage = CpuCounter.NextValue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error collecting CPU usage: {ex.Message}");
        }
    }
}