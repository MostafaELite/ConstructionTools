using System;
using System.Linq;
using System.Linq.Expressions;

namespace ConstructionTools.Repository.Abstract
{
    /// <summary>
    /// Represents an interface for generic repository that can be implemented in different ways
    /// </summary>
    /// <typeparam name="TEntity">The entity type that the operation should be preformed on</typeparam>
    public interface IRepository<TEntity>
    {
        //Commands
        void Add(TEntity newEntity);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        bool SaveChanges();

        //Queries
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Query();
    }
}