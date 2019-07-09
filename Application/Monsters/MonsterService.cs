using System.Collections.Generic;
using DL.Application.Infrastructure;
using DL.Domain.Monsters;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMonsterRepository _monsterRepository;

        public MonsterService(
            IUnitOfWork unitOfWork,
            IMonsterRepository monsterRepository
        )
        {
            _unitOfWork = unitOfWork;
            _monsterRepository = monsterRepository;
        }

        public Monster Find(int id)
        {
            Monster entity = null;
            _unitOfWork.Worker(() => 
            {
                entity = _monsterRepository.FindComplete(id);
            });
            return entity;
        }

        public List<Monster> All()
        {
            List<Monster> monsters = null;
            _unitOfWork.Worker(() => 
            {
                monsters = _monsterRepository.All();
            });
            return monsters;
        }

        public Monster Create(Monster monster)
        {
            Monster entity = null;
            _unitOfWork.Worker(() => 
            {
                entity = _monsterRepository.Add(monster);
                _unitOfWork.SaveChanges();
            });
            return entity;
        }

        public Monster AddReward(int monsterId, Reward reward)
        {
            Monster monster = null;
            _unitOfWork.Worker(() =>
            {
                monster = _monsterRepository.FindComplete(monsterId);
                monster.Rewards.Add(reward);
                _monsterRepository.Update(monster);
                _unitOfWork.SaveChanges();
            });
            return monster;
        }

        public void Remove(int id)
        {
            _unitOfWork.Worker(() => 
            {
                _monsterRepository.Remove(id);
                _unitOfWork.SaveChanges();
            });
        }
    }
}