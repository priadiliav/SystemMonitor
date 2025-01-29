using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.DataService.Repositories;

public class ComputerDetailsRepository(AppDbContext appDbContext, ILogger<ComputerDetailsRepository> logger)
    : GenericRepository<ComputerDetails>(appDbContext, logger), IComputerDetailsRepository
{
    public override async Task<IEnumerable<ComputerDetails>> Get()
    {
        try
        {
            return await DbSet.Where(x => x.Status == 0)
                .AsSplitQuery()
                .OrderBy(x => x.CreatingDateTime)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occured");
            throw; 
        }
    }

    public override async Task<bool> DeleteById(Guid id)
    {
        try
        {
            var existsComputerDetailsById = await DbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (existsComputerDetailsById == null)
                return false;

            existsComputerDetailsById.Status = 0;
            existsComputerDetailsById.UpdatingDateTime = DateTime.Now;
             
            return true;
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Error occured");
            throw;
        }

    }

    public override async Task<bool> Update(ComputerDetails computerDetails)
    {
        try
        {
            var existsComputerDetails = await DbSet.FirstOrDefaultAsync(x => x.Id == computerDetails.Id);

            if (existsComputerDetails == null)
                return false;

            existsComputerDetails.Name = computerDetails.Name;
            existsComputerDetails.UpdatingDateTime = DateTime.Now;

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occured");
            throw;
        }
    }

    public async Task<ComputerDetails?> GetByName(string name)
    {
        try
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occured");
            throw;
        }
    }
}
