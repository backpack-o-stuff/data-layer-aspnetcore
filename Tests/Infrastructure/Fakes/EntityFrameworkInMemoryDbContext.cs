using DL.Data;
using Microsoft.EntityFrameworkCore;

namespace DL.Tests.Infrastructure.Fakes
{
    public class EntityFrameworkInMemoryDbContext : ApplicationDbContext
    {
        private const string DB_NAME = "EF_INMEMORY_TEST_DB";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DB_NAME);
        }

        public EntityFrameworkInMemoryDbContext(string databaseConnectionString) 
            : base(databaseConnectionString) {}
    }
}