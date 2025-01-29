using Microsoft.Extensions.Logging;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;
using SystemMonitor.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SystemMonitor.DataService.Repositories;

public class UserRepository(AppDbContext appDbContext, ILogger<UserRepository> logger)
    : GenericRepository<User>(appDbContext, logger), IUserRepository
{
    public async Task<User?> GetByUserName(string userName)
    {
        if(string.IsNullOrEmpty(userName))
            throw new ArgumentNullException(nameof(userName), "User name is null or empty");
        
        try
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Username == userName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured");
            throw;
        }
    }
}