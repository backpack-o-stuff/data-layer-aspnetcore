using DL.Application.Domain.Scoreboards;

namespace DL.Application.Scoreboards
{
    public interface IScoreboardRepository
    {
        Scoreboard FindComplete(int id);
        Scoreboard Add(Scoreboard scoreboard);
        Scoreboard Update(Scoreboard scoreboard);
    }
}