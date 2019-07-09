using System.Collections.Generic;
using System.Linq;
using DL.Application.Monsters;
using DL.Data.Infrastructure.ContextControl;
using DL.Data.Infrastructure.Repositories;
using DL.Domain.Monsters;

namespace DL.Data.Monsters
{
    // INTENT
    //
    // Expose only what your application needs not everything on the base.
    // Handle any complex persistence logic in your repository layer. Assumingly
    // for a simple CRUD implementation most of these would be pass throughs to
    // the base.
    //
    // Additionally, the repository layer should have run the query by the time
    // it leaves here. The RepositoryBase deals in Queryable, the Repository in
    // Lists. We should not be querying in the application/services layer.
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
            return RetrieveReadonlyAll<Monster>().ToList();
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