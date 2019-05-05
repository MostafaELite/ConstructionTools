using ConstructionTools.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ConstructionTools.Repository.Concreate
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _ctx;
        private readonly DbSet<TEntity> _entities;
        public SqlRepository(DbContext ctx)
        {
            _ctx = ctx;
            _entities = _ctx.Set<TEntity>();
            _ctx.Database.EnsureCreated();
        }

        /// <summary>
        /// Adds a new record to the database , changes will not be save until you cal SaveChanges method
        /// </summary>
        /// <param name="newEntity">the new record to add</param>
        public void Add(TEntity newEntity) =>
            _entities.Add(newEntity);

        /// <summary>
        /// Removes a record from the database, changes will not be save until you cal SaveChanges method
        /// </summary>
        /// <param name="entityToDelete">the record to be deleted</param>
        public void Delete(TEntity entityToDelete) =>
            _entities.Remove(entityToDelete);

        public void Update(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an IQueryable That match a specific condition
        /// </summary>
        /// <param name="predicate">the condition for retreving records from the database</param>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) =>
            _entities.Where(predicate);

        /// <summary>
        /// Returns an IQueryable That contains all record in one table
        /// </summary>        
        public IQueryable<TEntity> Query() =>
            _entities;

        /// <summary>
        /// Save pending changes (add , update , delete)
        /// </summary>
        /// <returns>a boolean indicating in case the operation performed any change to the database records (added new record , deleted or updated existing one )</returns>
        public bool SaveChanges() => _ctx.SaveChanges() > 0;

        /// <summary>
        /// Returns the same object after being attached to the context and ready for further operations (mostly used in updating existing records)
        /// </summary>
        /// <param name="entityToAttach">an object which should be attached to the DbContext</param>
        /// <param name="isModified">a flag indicating whether this object has modified (updated values) that should be saved</param>
        /// <returns>an object attached to the context</returns>
        public EntityEntry<TEntity> Attach(TEntity entityToAttach, bool isModified = false)
        {
            var attachedEntity = _ctx.Attach(entityToAttach);
            if (isModified)
                attachedEntity.State = EntityState.Modified;
            return attachedEntity;
        }



    }
}