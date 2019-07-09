using Newtonsoft.Json;

namespace DL.Domain.Scoreboards
{
    public class ScoreboardEntry : IEntity
    {
        public int Id { get; set; }
        public int MonsterId { get; set; }
        public int PlayersDefeated { get; set; }

        [JsonIgnore]
        public Scoreboard Scoreboard { get; set; }
        public int ScoreboardId { get; set; }
    }
}