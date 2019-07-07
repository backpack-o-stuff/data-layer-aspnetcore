using DL.Data.EF.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DL.Tests.Infrastructure.Fakes
{
    public class EntityFrameworkInMemoryDbContext : ApplicationContext
    {
        private const string DB_NAME = "EF_INMEMORY_TEST_DB";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DB_NAME);
        }
    }
}