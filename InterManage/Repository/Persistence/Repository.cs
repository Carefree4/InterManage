using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }
        public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().Where(predicate);

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().SingleOrDefault(predicate);

        public void Add(TEntity entity) => Context.Set<TEntity>().Add(entity);

        public void AddRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().AddRange(entities);


        public void Remove(TEntity entity)
            =>
            Context.Set<TEntity>().Remove(entity);


        public void RemoveRange(IEnumerable<TEntity> entities)
            =>
            Context.Set<TEntity>().RemoveRange(entities);
    }
}