using SystemMonitor.Models.Entities;

namespace SystemMonitor.DataService.Contracts;

public interface IComputerMetricsRepository : IGenericRepository<ComputerMetrics>
{
    Task<ComputerMetrics?> GetComputerMetricsByComputerId(Guid computerDetailsId);
}