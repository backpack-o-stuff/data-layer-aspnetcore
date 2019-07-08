using System;

namespace DL.Application.Infrastructure
{
    public interface IRepositoryWorker
    {
        void Worker(Action work);
        void TransactionWorker(Action work);
        void SaveChanges();
    }
}