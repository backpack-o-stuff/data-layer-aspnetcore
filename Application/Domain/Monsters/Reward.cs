using Newtonsoft.Json;

namespace DL.Application.Domain.Monsters
{
    public class Reward : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        [JsonIgnore]
        public Monster Monster { get; set; }
        public int MonsterId { get; set; }
    }
}