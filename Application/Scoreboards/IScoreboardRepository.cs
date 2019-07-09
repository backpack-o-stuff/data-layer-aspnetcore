using DL.Application.Domain.Scoreboards;
using DL.Application.Infrastructure;

namespace DL.Application.Scoreboards
{
    public interface IScoreboardRepository : IRepositoryWorker
    {
        Scoreboard FindComplete(int id);
        Scoreboard Add(Scoreboard scoreboard);
        Scoreboard Update(Scoreboard scoreboard);
    }
}