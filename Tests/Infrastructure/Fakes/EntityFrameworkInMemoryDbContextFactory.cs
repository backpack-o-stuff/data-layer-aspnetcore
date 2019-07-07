using DL.Data.EF.Infrastructure;

namespace DL.Tests.Infrastructure.Fakes
{
    public class EntityFrameworkInMemoryDbContextFactory : IDbContextFactory
    {
        public ApplicationContext For()
        {
            return new EntityFrameworkInMemoryDbContext();
        }
    }
}