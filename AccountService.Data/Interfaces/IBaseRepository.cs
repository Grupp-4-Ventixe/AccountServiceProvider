using System.Linq.Expressions;

namespace AccountService.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task<T?> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<bool> UpdateAsync(T entity);
    }
}