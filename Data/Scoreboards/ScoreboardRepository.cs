using System.Collections.Generic;
using System.Linq;
using DL.Application.Scoreboards;
using DL.Data.Infrastructure.ContextControl;
using DL.Data.Infrastructure.Repositories;
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

        public List<Scoreboard> All()
        {
            return RetrieveReadonlyAll<Scoreboard>().ToList();
        }

        public Scoreboard Add(Scoreboard scoreboard)
        {
            return AddEntity(scoreboard);
        }

        public Scoreboard Update(Scoreboard scoreboard)
        {
            return UpdateEntity(scoreboard);
        }

        public void RemoveRange(List<Scoreboard> scoreboards)
        {
            RemoveEntityBy(scoreboards);
        }
    }
}