using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;

namespace SystemMonitor.DataService.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ILogger<GenericRepository<T>> Logger;
    internal readonly DbSet<T> DbSet;
    protected GenericRepository(
        AppDbContext appDbContext,
        ILogger<GenericRepository<T>> logger)
    {
        Logger = logger;
        DbSet = appDbContext.Set<T>();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await DbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> DeleteById(Guid id)
    {
        throw new NotImplementedException("Not implemented generic method");
    }

    public virtual Task<IEnumerable<T>> Get()
    {
        throw new NotImplementedException("Not implemented generic method");
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual Task<bool> Update(T entity)
    {
        throw new NotImplementedException("Not implemented generic method");
    }
}
