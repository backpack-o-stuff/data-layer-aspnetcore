using DL.Application.Domain.Scoreboards;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Scoreboards
{
    public class ScoreboardModelBuilder
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Scoreboard>();
            entity.ToTable("scoreboards");

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("INTEGER")
                .IsRequired();
            entity.HasKey(x => x.Id);

            entity.HasMany(x => x.ScoreboardEntries)
                .WithOne(c => c.Scoreboard)
                .HasForeignKey(x => x.ScoreboardId);
        }
    }
}