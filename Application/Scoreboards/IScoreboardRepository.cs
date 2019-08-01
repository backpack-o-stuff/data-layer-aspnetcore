using System.Collections.Generic;
using DL.Domain.Scoreboards;

namespace DL.Application.Scoreboards
{
    public interface IScoreboardRepository
    {
        Scoreboard FindComplete(int id);
        List<Scoreboard> All();
        Scoreboard Add(Scoreboard scoreboard);
        Scoreboard Update(Scoreboard scoreboard);
        void RemoveRange(List<Scoreboard> scoreboards);
    }
}