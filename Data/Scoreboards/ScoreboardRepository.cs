using DL.Application.Domain.Scoreboards;
using DL.Application.Scoreboards;
using DL.Data.Infrastructure;

namespace DL.Data.Scoreboards
{
    public class ScoreboardRepository : RepositoryBase<Scoreboard>, IScoreboardRepository
    {
        public ScoreboardRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory) {}
            
        public Scoreboard FindComplete(int id)
        {
            return FindBy(
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