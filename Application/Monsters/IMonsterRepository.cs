using System.Collections.Generic;
using DL.Application.Domain.Monsters;
using DL.Application.Infrastructure;

namespace DL.Application.Monsters
{
    public interface IMonsterRepository : IRepositoryWorker
    {
        Monster Find(int id);
        List<Monster> All();
        Monster Add(Monster monster);
        void Remove(int id);
        void RemoveRange(List<Monster> monsters);
    }
}