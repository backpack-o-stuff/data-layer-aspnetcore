using System;
using DL.Application.Domain;
using DL.Application.Infrastructure;

namespace DL.Data.Infrastructure
{
    public class RepositoryBase<T> : IRepositoryWorker
        where T : IEntity
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
    }
}