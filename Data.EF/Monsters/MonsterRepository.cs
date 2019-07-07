using System.Collections.Generic;
using System.Linq;
using DL.Application.Domain.Monsters;
using DL.Application.Monsters;
using DL.Data.EF.Infrastructure;

namespace DL.Data.EF.Monsters
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public MonsterRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Monster Find(int id)
        {
            using(var context = _dbContextFactory.For())
            {
                return context.Monsters.FirstOrDefault(x => x.Id == id);       
            }
        }

        public List<Monster> All()
        {
            using(var context = _dbContextFactory.For())
            {
                return context.Monsters.ToList();       
            }
        }

        public Monster Add(Monster monster)
        {
            using(var context = _dbContextFactory.For())
            {
                context.Monsters.Add(monster);       
                context.SaveChanges();
                return monster;
            }
        }

        public void Remove(int id)
        {
            using(var context = _dbContextFactory.For())
            {
                var monster = context.Monsters.FirstOrDefault(x => x.Id == id);
                context.Monsters.Remove(monster);       
                context.SaveChanges();
            }
        }

        public void RemoveRange(List<Monster> monsters)
        {
            using(var context = _dbContextFactory.For())
            {
                context.Monsters.RemoveRange(monsters);       
                context.SaveChanges();
            }
        }
    }
}