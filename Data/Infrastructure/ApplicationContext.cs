using DL.Data.Monsters;
using DL.Data.Scoreboards;
using DL.Domain.Monsters;
using DL.Domain.Scoreboards;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private readonly string _databaseConnectionString;

        public ApplicationContext(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_databaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MonsterModelBuilder.Configure(modelBuilder);
            RewardModelBuilder.Configure(modelBuilder);
            ScoreboardModelBuilder.Configure(modelBuilder);
            ScoreboardEntryModelBuilder.Configure(modelBuilder);
        }

        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Scoreboard> Scoreboards { get; set; }
        public DbSet<ScoreboardEntry> ScoreboardEntries { get; set; }
    }
}