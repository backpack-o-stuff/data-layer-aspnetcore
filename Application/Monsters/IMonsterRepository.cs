using System.Collections.Generic;
using DL.Application.Domain.Monsters;

namespace DL.Application.Monsters
{
    public interface IMonsterRepository
    {
        Monster Find(int id);
        List<Monster> All();
        Monster Add(Monster monster);
        void Remove(int id);
        void RemoveRange(List<Monster> monsters);
    }
}