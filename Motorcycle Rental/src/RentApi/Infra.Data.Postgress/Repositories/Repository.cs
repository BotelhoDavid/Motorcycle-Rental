using Microsoft.EntityFrameworkCore;
using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Infra.Data.Postgress.Context;
using System.Linq.Expressions;

namespace Rent.Infra.Data.Postgress.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Create
        public async Task<TEntity> CreateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            await _dbSet.AddAsync(model);
            return model;
        }

        //Update
        public async Task<bool> UpdateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dbSet.Attach(model);
            _context.Entry(model).State = EntityState.Modified;

            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(params object[] keys)
        {
            var model = await _dbSet.FindAsync(keys);

            if (model == null)
                return false;

            _dbSet.Remove(model);
            return true; 
        }

        // Get
        public async Task<TEntity?> GetAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        // Query
        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        // Commit
        public async Task<bool> CommitAsync()
        {
            try
            {
                var committed = await _context.SaveChangesAsync();
                return committed > 0;

            }
            catch (Exception err)
            {

                return false;
            }
        }

        // Dispose
        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
