using DL.Data;
using DL.Data.Infrastructure.ContextControl;

namespace DL.Tests.Infrastructure.Fakes
{
    public class EntityFrameworkInMemoryDbContextFactory : IDbContextFactory
    {
        public ApplicationDbContext For()
        {
            return new EntityFrameworkInMemoryDbContext("DatabaseConnectionStringNotNeeded");
        }
    }
}