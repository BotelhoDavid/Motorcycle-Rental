using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rent.Domain.Interfaces.Repositories.Postgres
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        // Create
        Task<TEntity> CreateAsync(TEntity model);

        //Update
        Task<bool> UpdateAsync(TEntity model);

        // Delete
        Task<bool> DeleteAsync(params object[] keys);

        // Get
        Task<TEntity?> GetAsync(params object[] keys);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

        // Query
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        // Commit
        Task<bool> CommitAsync();
    }
}