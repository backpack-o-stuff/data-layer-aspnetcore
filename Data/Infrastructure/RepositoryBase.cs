using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DL.Application.Domain;
using DL.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Infrastructure
{
    public class RepositoryBase<TEntity> : IRepositoryWorker
        where TEntity : class, IEntity
    {
        private readonly IDbContextFactory _dbContextFactory;
        private ApplicationContext _context;

        public RepositoryBase(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void Worker(Action work)
        {
            _context = _dbContextFactory.For();
            using(Context()) 
                work();
        }

        public void TransactionWorker(Action work)
        {
            Exception exception = null;

            _context = _dbContextFactory.For();
            using(var transaction = Context().Database.BeginTransaction())
            {
                try
                {
                    using(Context()) 
                        work();

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    exception = e;
                    transaction.Rollback();
                }
            }

            if(exception != null)
                throw exception;
        }

        public void SaveChanges()
        {
            Context().SaveChanges();
        }

        protected ApplicationContext Context()
        {
            if(_context == null)
                throw new Exception("Context is not initialized. Please use your Worker or TransactionWorker helpers");

            return _context;
        }

        protected TEntity FindBy(Func<TEntity, bool> selector)
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.FirstOrDefault(selector);
        }

        protected TEntity FindReadonlyBy(Func<TEntity, bool> selector)
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking().FirstOrDefault(selector);
        }

        protected TEntity FindBy(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.FirstOrDefault(selector);
        }

        protected TEntity FindReadonlyBy(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.AsNoTracking().FirstOrDefault(selector);
        }

        protected List<TEntity> RetrieveAll()
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.ToList();
        }
        
        protected List<TEntity> RetrieveReadonlyAll()
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking().ToList();
        }
        
        protected List<TEntity> RetrieveAllBy(Func<TEntity, bool> selector)
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.Where(selector).ToList();
        }
        
        protected List<TEntity> RetrieveReadonlyAllBy(Func<TEntity, bool> selector)
        {
            var dbSet = Context().Set<TEntity>();
            return dbSet.AsNoTracking().Where(selector).ToList();
        }

        protected List<TEntity> RetrieveAllBy(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.Where(selector).ToList();
        }

        protected List<TEntity> RetrieveReadonlyAllBy(
            Func<TEntity, bool> selector,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            var dbSet = Context().Set<TEntity>();
            IQueryable<TEntity> set = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dbSet, (current, expression) => current.Include(expression));
            return set.AsNoTracking().Where(selector).ToList();
        }

        protected TEntity AddEntity(TEntity entity)
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.Add(entity);
            return entity;
        }
        
        protected List<TEntity> AddEntityRange(List<TEntity> entities)
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.AddRange(entities);
            return entities;
        }

        protected TEntity UpdateEntity(TEntity entity)
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.Update(entity);
            return entity;
        }
        
        protected List<TEntity> UpdateEntityRange(List<TEntity> entities)
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.UpdateRange(entities);
            return entities;
        }
        
        protected void RemoveEntity(int id)
        {
            var dbSet = Context().Set<TEntity>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
                
        protected void RemoveEntityBy(Func<TEntity, bool> selector)
        {
            var dbSet = Context().Set<TEntity>();
            var entities = dbSet.Where(selector);
            dbSet.RemoveRange(entities);
        }
        
        protected void RemoveEntityBy(List<TEntity> entities)
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.RemoveRange(entities);
        }
                        
        protected void RemoveEntityAll()
        {
            var dbSet = Context().Set<TEntity>();
            dbSet.RemoveRange(dbSet);
        }
    }
}