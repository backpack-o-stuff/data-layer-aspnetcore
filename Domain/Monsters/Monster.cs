using System.Collections.Generic;

namespace DL.Domain.Monsters
{
    public class Monster : IEntity
    {
        public Monster()
        {
            Rewards = new List<Reward>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }

        public List<Reward> Rewards { get; set; }
    }
}