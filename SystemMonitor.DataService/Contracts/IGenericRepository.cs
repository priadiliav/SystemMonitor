namespace SystemMonitor.DataService.Contracts;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> Get();
    Task<T?> GetById(Guid id);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> DeleteById(Guid id);
}
