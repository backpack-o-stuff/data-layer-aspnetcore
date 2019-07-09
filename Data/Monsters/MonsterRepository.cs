using System.Collections.Generic;
using DL.Application.Monsters;
using DL.Data.Infrastructure;
using DL.Domain.Monsters;

namespace DL.Data.Monsters
{
    // INTENT
    // expose only what your application needs not everything on the base.
    // Handle any complex persistence logic in your repository layer. Assumingly
    // for a simple CRUD implementation most of these would be pass throughs to
    // the base.
    public class MonsterRepository : RepositoryBase, IMonsterRepository
    {
        public MonsterRepository(IContextSessionProvider contextSessionProvider)
            : base(contextSessionProvider) {}

        public Monster FindComplete(int id)
        {
            return FindBy<Monster>(
                x => x.Id == id,
                x => x.Rewards);
        }

        public List<Monster> All()
        {
            // NOTE: example of readonly use, not here for any particular reason.
            return RetrieveReadonlyAll<Monster>();
        }

        public Monster Add(Monster monster)
        {
            return AddEntity(monster);
        }

        public Monster Update(Monster monster)
        {
            return UpdateEntity(monster);
        }

        public void Remove(int id)
        {
            RemoveEntity<Monster>(id);
        }

        public void RemoveRange(List<Monster> monsters)
        {
            RemoveEntityBy(monsters);
        }
    }
}