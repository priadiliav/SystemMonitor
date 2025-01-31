using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Collectors;

public class DiskUsageCollector : IMetricCollector
{
    public void Collect(ComputerMetricsDto metrics)
    {
        try
        {
            var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady).ToList();

            double totalUsedSpace = 0;
            double totalDiskSpace = 0;

            foreach (var drive in drives)
            {
                totalUsedSpace += drive.TotalSize - drive.AvailableFreeSpace;
                totalDiskSpace += drive.TotalSize;
            }

            metrics.DiskUsage = totalDiskSpace > 0 ? totalUsedSpace / totalDiskSpace * 100 : 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error collecting Disk usage: {ex.Message}");
        }
    }
}