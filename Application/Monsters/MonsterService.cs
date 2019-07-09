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
        Monster Create(Monster monster);
        Monster AddReward(int monsterId, Reward reward);
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
                entity = _monsterRepository.FindComplete(id);
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

        public Monster Create(Monster monster)
        {
            Monster entity = null;
            _monsterRepository.Worker(() => 
            {
                entity = _monsterRepository.Add(monster);
                _monsterRepository.SaveChanges();
            });
            return entity;
        }

        public Monster AddReward(int monsterId, Reward reward)
        {
            Monster monster = null;
            _monsterRepository.Worker(() =>
            {
                monster = _monsterRepository.FindComplete(monsterId);
                monster.Rewards.Add(reward);
                _monsterRepository.Update(monster);
                _monsterRepository.SaveChanges();
            });
            return monster;
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