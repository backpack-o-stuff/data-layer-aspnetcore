using System;
using DL.Application.Infrastructure;

namespace DL.Data.Infrastructure.ContextControl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextSessionProvider _contextSessionProvider;

        public UnitOfWork(IContextSessionProvider contextSessionProvider)
        {
            _contextSessionProvider = contextSessionProvider;
        }

        public void Worker(Action work)
        {
            using(_contextSessionProvider.ContextSession()) 
                work();
            _contextSessionProvider.Dispose();
        }

        public void TransactionWorker(Action work)
        {
            Exception exception = null;

            var context = Context();
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    using(context) 
                    {
                        work();
                        transaction.Commit();
                    }
                }
                catch(Exception e)
                {
                    exception = e;
                    transaction.Rollback();
                }
            }
            _contextSessionProvider.Dispose();

            if(exception != null)
                throw exception;
        }

        public void SaveChanges()
        {
            Context().SaveChanges();
        }

        private ApplicationDbContext Context()
        {
            return _contextSessionProvider.ContextSession();
        }
    }
}