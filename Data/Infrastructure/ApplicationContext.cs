using DL.Application.Domain.Monsters;
using DL.Data.Monsters;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private const string DB_SOURCE = "../Data/entityframework-local.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ DB_SOURCE }");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MonsterModelBuilder.Configure(modelBuilder);
        }

        public DbSet<Monster> Monsters { get; set; }
    }
}