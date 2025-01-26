namespace SystemMonitor.DataService.Contracts;
public interface IUnitOfWork
{
    IComputerDetailsRepository ComputerDetails { get; set; }
    IComputerMetricsRepository ComputerMetrics { get; set; }
    Task<bool> CompleteTask();
}
