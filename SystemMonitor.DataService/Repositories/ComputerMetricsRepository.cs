using Microsoft.Extensions.Logging;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;
using SystemMonitor.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SystemMonitor.DataService.Repositories;
public class ComputerMetricsRepository(AppDbContext appDbContext, ILogger<ComputerMetricsRepository> logger)
    : GenericRepository<ComputerMetrics>(appDbContext, logger), IComputerMetricsRepository
{
    public async Task<ComputerMetrics?> GetComputerMetricsByComputerId(Guid computerDetailsId)
    {
        try
        {
            return await DbSet.FirstOrDefaultAsync(x => x.ComputerDetailsId == computerDetailsId);
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Error occured"); 
            throw;
        }
    }

    public override async Task<bool> Update(ComputerMetrics entity)
    {
        try
        {
            var result = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null)
                return false;

            result.CpuUsage = entity.CpuUsage;
            result.DiskUsage = entity.DiskUsage;
            result.RamUsage = entity.RamUsage;
            result.NetworkUsage = entity.NetworkUsage;
            result.UpdatingDateTime = DateTime.Now;
            
            return true;
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Error occured");
            throw;
        }
    }

}
