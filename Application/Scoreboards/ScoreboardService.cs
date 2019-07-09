using DL.Application.Infrastructure;
using DL.Application.Monsters;
using DL.Domain.Scoreboards;

namespace DL.Application.Scoreboards
{
    public interface IScoreboardService
    {
        Scoreboard CreateScoreboard();
    }

    public class ScoreboardService : IScoreboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScoreboardRepository _scoreboardRepository;
        private readonly IMonsterRepository _monsterRepository;

        public ScoreboardService(
            IUnitOfWork unitOfWork, 
            IScoreboardRepository scoreboardRepository,
            IMonsterRepository monsterRepository
        )
        {
            _unitOfWork = unitOfWork;
            _scoreboardRepository = scoreboardRepository;
            _monsterRepository = monsterRepository;
        }

        public Scoreboard CreateScoreboard()
        {
            Scoreboard scoreboard = null;
            _unitOfWork.Worker(() => 
            {
                scoreboard = new Scoreboard();
                scoreboard = _scoreboardRepository.Add(scoreboard);
                _unitOfWork.SaveChanges();

                var monsters = _monsterRepository.All();
                monsters.ForEach(monster => 
                {
                    scoreboard.ScoreboardEntries.Add(new ScoreboardEntry
                    {
                        MonsterId = monster.Id,
                        PlayersDefeated = 0
                    });
                });
                scoreboard = _scoreboardRepository.Update(scoreboard);

                _unitOfWork.SaveChanges();
            });
            return scoreboard;
        }
    }
}