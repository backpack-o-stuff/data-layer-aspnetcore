// NOTE: Domain would not normally exist in the Application Layer. 
// This is just simplicity sake of example.

namespace DL.Application.Domain.Monsters
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
    }
}