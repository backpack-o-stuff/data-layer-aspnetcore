using System;
using System.Collections.Generic;
using System.Linq;
using DL.Application.Domain.Monsters;
using DL.Application.Monsters;
using DL.Data.Infrastructure;

namespace DL.Data.Monsters
{
    public class MonsterRepository : RepositoryBase<Monster>, IMonsterRepository
    {
        public MonsterRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory) {}

        public Monster Find(int id)
        {
            return Context().Monsters.FirstOrDefault(x => x.Id == id);       
        }

        public List<Monster> All()
        {
            return Context().Monsters.ToList();
        }

        public Monster Add(Monster monster)
        {
            Context().Monsters.Add(monster);       
            return monster;
        }

        public void Remove(int id)
        {
            var monster = Context().Monsters.FirstOrDefault(x => x.Id == id);
            if(monster == null)
                return;

            Context().Monsters.Remove(monster);       
        }

        public void RemoveRange(List<Monster> monsters)
        {
            Context().Monsters.RemoveRange(monsters);       
        }
    }
}