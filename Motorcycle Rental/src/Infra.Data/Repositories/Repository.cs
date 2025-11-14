using Domain.Interfaces.Repositories;
using Infra.Context;
using MongoDB.Driver;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public class Repository<TDocument> : IRepository<TDocument> where TDocument : class
    {
        protected readonly IMongoDBContext _context;

        protected IMongoCollection<TDocument> Collection
        {
            get
            {
                return _context.DataBase.GetCollection<TDocument>(typeof(TDocument)?.Name);
            }
        }

        public Repository(IMongoDBContext context)
        {
            _context = context;
        }

        #region Métodos Síncronos

        public IQueryable<TDocument> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).ToEnumerable();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public IQueryable<TDocument> QueryPagedAndSortDynamic(Expression<Func<TDocument, bool>> filterExpression,
                                                              string sotyByPropertie,
                                                              int page,
                                                              int itemsPerPage)
        {
            IQueryable<TDocument> _filtered = Collection.AsQueryable().Where(filterExpression);

            _filtered = _filtered.OrderBy(sotyByPropertie)
                                 .Skip(page * itemsPerPage)
                                 .Take(itemsPerPage);

            return _filtered;
        }

        public TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).FirstOrDefault();
        }

        public void InsertOne(TDocument document)
        {
            Collection.InsertOne(document);
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            Collection.InsertMany(documents);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            Collection.FindOneAndDelete(filterExpression);
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            Collection.DeleteMany(filterExpression);
        }

        #endregion

        #region Métodos Assíncronos

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => Collection.InsertOneAsync(document));
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await Collection.InsertManyAsync(documents);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.FindOneAndDeleteAsync(filterExpression));
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.DeleteManyAsync(filterExpression));
        }

        #endregion
    }
}