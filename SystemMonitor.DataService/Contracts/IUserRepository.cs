using SystemMonitor.Models.Entities;

namespace SystemMonitor.DataService.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUserName(string userName);
}