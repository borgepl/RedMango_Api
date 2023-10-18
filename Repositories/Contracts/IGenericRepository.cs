using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllFilteredAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}
