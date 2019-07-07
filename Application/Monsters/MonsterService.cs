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
            return _monsterRepository.Find(id);
        }

        public List<Monster> All()
        {
            return _monsterRepository.All();
        }

        public Monster Add(Monster monster)
        {
            return _monsterRepository.Add(monster);
        }

        public void Remove(int id)
        {
            _monsterRepository.Remove(id);
        }
    }
}