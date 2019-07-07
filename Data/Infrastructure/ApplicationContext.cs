using DL.Application.Domain.Monsters;
using DL.Data.EF.Monsters;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.EF.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=entityframework-local.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MonsterModelBuilder.Configure(modelBuilder);
        }

        public DbSet<Monster> Monsters { get; set; }
    }
}