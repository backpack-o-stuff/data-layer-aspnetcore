namespace DL.Application.Domain.Monsters
{
    public class Monster : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
    }
}