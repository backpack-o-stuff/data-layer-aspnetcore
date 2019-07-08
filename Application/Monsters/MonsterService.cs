using System.Collections.Generic;
using DL.Application.Domain.Monsters;

namespace DL.Application.Monsters
{
    /// <summary>
    /// Meaningless service that just passes through to the repository.
    /// </summary>
    public interface IMonsterService
    {
        Monster Find(int id);
        List<Monster> All();
        Monster Add(Monster monster);
        void Remove(int id);
    }

    public class MonsterService : IMonsterService
    {
        private readonly IMonsterRepository _monsterRepository;

        public MonsterService(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public Monster Find(int id)
        {
            Monster entity = null;
            _monsterRepository.Worker(() => 
            {
                entity = _monsterRepository.Find(id);
            });
            return entity;
        }

        public List<Monster> All()
        {
            List<Monster> monsters = null;
            _monsterRepository.Worker(() => 
            {
                monsters = _monsterRepository.All();
            });
            return monsters;
        }

        public Monster Add(Monster monster)
        {
            Monster entity = null;
            _monsterRepository.Worker(() => 
            {
                entity = _monsterRepository.Add(monster);
                _monsterRepository.SaveChanges();
            });
            return entity;
        }

        public void Remove(int id)
        {
            _monsterRepository.Worker(() => 
            {
                _monsterRepository.Remove(id);
                _monsterRepository.SaveChanges();
            });
        }
    }
}