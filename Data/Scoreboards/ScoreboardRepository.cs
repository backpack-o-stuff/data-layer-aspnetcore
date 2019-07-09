using DL.Application.Scoreboards;
using DL.Data.Infrastructure;
using DL.Domain.Scoreboards;

namespace DL.Data.Scoreboards
{
    public class ScoreboardRepository : RepositoryBase, IScoreboardRepository
    {
        public ScoreboardRepository(IContextSessionProvider contextSessionProvider)
            : base(contextSessionProvider) {}
            
        public Scoreboard FindComplete(int id)
        {
            return FindBy<Scoreboard>(
                x => x.Id == id,
                x => x.ScoreboardEntries);
        }

        public Scoreboard Add(Scoreboard scoreboard)
        {
            return AddEntity(scoreboard);
        }

        public Scoreboard Update(Scoreboard scoreboard)
        {
            return UpdateEntity(scoreboard);
        }
    }
}