using DL.Application.Domain.Scoreboards;
using DL.Application.Monsters;

namespace DL.Application.Scoreboards
{
    public interface IScoreboardService
    {
        Scoreboard CreateScoreboard();
    }

    public class ScoreboardService : IScoreboardService
    {
        private readonly IScoreboardRepository _scoreboardRepository;
        private readonly IMonsterRepository _monsterRepository;

        public ScoreboardService(
            IScoreboardRepository scoreboardRepository,
            IMonsterRepository monsterRepository
        )
        {
            _scoreboardRepository = scoreboardRepository;
            _monsterRepository = monsterRepository;
        }

        public Scoreboard CreateScoreboard()
        {
            Scoreboard scoreboard = null;
            _scoreboardRepository.Worker(() => 
            {
                scoreboard = new Scoreboard();
                scoreboard = _scoreboardRepository.Add(scoreboard);

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

                _scoreboardRepository.SaveChanges();
            });
            return scoreboard;
        }
    }
}