using SystemMonitor.Models.Entities;

namespace SystemMonitor.DataService.Contracts;

public interface IComputerDetailsRepository : IGenericRepository<ComputerDetails>
{
    Task<ComputerDetails?> GetByName(string name);
}
