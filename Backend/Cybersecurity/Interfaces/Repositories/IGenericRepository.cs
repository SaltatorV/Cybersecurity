using System.Linq.Expressions;

namespace Cybersecurity.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        public Task SaveAsync();

    }
}
