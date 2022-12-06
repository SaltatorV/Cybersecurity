using System.Linq.Expressions;
using Cybersecurity.Data;
using Cybersecurity.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cybersecurity.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal CybersecurityDbContext Context;
        internal DbSet<T> DbSet;

        public GenericRepository(CybersecurityDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await DbSet.FindAsync(id);

            return await Task.FromResult(result);
        }

        public async Task InsertAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await DbSet.Where(predicate).FirstOrDefaultAsync();

            return await Task.FromResult(result);
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await DbSet.AnyAsync(predicate);

            return await Task.FromResult(result);
        }

        public async Task DeleteAsync(int id)
        {
            T existing = DbSet.Find(id);
            DbSet.Remove(existing);
            await Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
            await Task.CompletedTask;

        }
    }
}
