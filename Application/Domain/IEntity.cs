// NOTE: Domain would not normally exist in the Application Layer. 
// This is just simplicity sake of example.

namespace DL.Application.Domain
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}