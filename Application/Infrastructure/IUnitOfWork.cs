using System;

namespace DL.Application.Infrastructure
{
    public interface IUnitOfWork
    {
        void Worker(Action work);
        void TransactionWorker(Action work);
        void SaveChanges();
    }
}