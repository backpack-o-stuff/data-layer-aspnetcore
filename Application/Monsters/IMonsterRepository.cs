using System.Collections.Generic;
using DL.Application.Domain.Monsters;

namespace DL.Application.Monsters
{
    public interface IMonsterRepository
    {
        Monster FindComplete(int id);
        List<Monster> All();
        Monster Add(Monster monster);
        Monster Update(Monster monster);
        void Remove(int id);
        void RemoveRange(List<Monster> monsters);
    }
}