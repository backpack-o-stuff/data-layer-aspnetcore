using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DL.Data.Infrastructure.ContextControl;
using DL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Infrastructure.Repositories
{
    public class RepositoryBase
    {
        private readonly IContextSessionProvider _contextSessionProvider;

        public RepositoryBase(IContextSessionProvider contextSessionProvider)
        {
            _contextSessionProvider = contextSessionProvider;
        }

        protected ApplicationDbContext Context()
        {
            return _contextSessionProvider.ContextSession();
        }

        protected TEntity FindBy<TEntity>(Func<TEntity, bool> selector)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.FirstOrDefault(selector);
        }

        protected TEntity FindReadonlyBy<TEntity>(Func<TEntity, bool> selector)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking().FirstOrDefault(selector);
        }

        protected TEntity FindBy<TEntity>(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.FirstOrDefault(selector);
        }

        protected TEntity FindReadonlyBy<TEntity>(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.AsNoTracking().FirstOrDefault(selector);
        }

        protected IQueryable<TEntity> RetrieveAll<TEntity>()
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet;
        }
        
        protected IQueryable<TEntity> RetrieveReadonlyAll<TEntity>()
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking();
        }
        
        protected IQueryable<TEntity> RetrieveAllBy<TEntity>(Func<TEntity, bool> selector)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.Where(selector).AsQueryable();
        }
        
        protected IQueryable<TEntity> RetrieveReadonlyAllBy<TEntity>(Func<TEntity, bool> selector)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking().Where(selector).AsQueryable();
        }

        protected IQueryable<TEntity> RetrieveAllBy<TEntity>(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.Where(selector).AsQueryable();
        }

        protected IQueryable<TEntity> RetrieveReadonlyAllBy<TEntity>(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.AsNoTracking().Where(selector).AsQueryable();
        }

        protected TEntity AddEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.Add(entity);
            return entity;
        }
        
        protected List<TEntity> AddEntityRange<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.AddRange(entities);
            return entities;
        }

        protected TEntity UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.Update(entity);
            return entity;
        }
        
        protected List<TEntity> UpdateEntityRange<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.UpdateRange(entities);
            return entities;
        }
        
        protected void RemoveEntity<TEntity>(int id)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
                
        protected void RemoveEntityBy<TEntity>(Func<TEntity, bool> selector)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            var entities = dbSet.Where(selector);
            dbSet.RemoveRange(entities);
        }
        
        protected void RemoveEntityBy<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.RemoveRange(entities);
        }
                        
        protected void RemoveEntityAll<TEntity>()
            where TEntity : class, IEntity
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.RemoveRange(dbSet);
        }
    }
}