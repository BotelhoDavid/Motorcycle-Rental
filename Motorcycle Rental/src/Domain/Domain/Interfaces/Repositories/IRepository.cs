using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TDocument> where TDocument : class
    {
        #region Métodos Síncronos

        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        void InsertOne(TDocument document);

        void InsertMany(ICollection<TDocument> documents);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        #endregion

        #region Métodos Assíncronos

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task InsertOneAsync(TDocument document);

        Task InsertManyAsync(ICollection<TDocument> documents);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

        #endregion
    }
}
