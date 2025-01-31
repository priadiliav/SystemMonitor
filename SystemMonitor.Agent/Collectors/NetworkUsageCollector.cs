using System.Net.NetworkInformation;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Collectors;

public class NetworkUsageCollector : IMetricCollector
{
    public void Collect(ComputerMetricsDto metrics)
    {
        try
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            if (networkInterface == null)
            {
                metrics.NetworkUsage = 0;
                return;
            }

            var bytesSent = networkInterface.GetIPv4Statistics().BytesSent;
            var bytesReceived = networkInterface.GetIPv4Statistics().BytesReceived;

            metrics.NetworkUsage = (bytesReceived + bytesSent) / 1024;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error collecting Network usage: {ex.Message}");
        }
    }
}