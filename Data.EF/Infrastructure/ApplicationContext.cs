using System.Reflection;
using DL.Application.Domain.Monsters;
using DL.Data.EF.Monsters;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.EF.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private const string DB_SOURCE = "../Data.EF/entityframework-local.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ DB_SOURCE }", options =>
            {
                // TODO: explore why this isn't working.
                options.MigrationsAssembly("DL.Data.EF");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MonsterModelBuilder.Configure(modelBuilder);
        }

        public DbSet<Monster> Monsters { get; set; }
    }
}