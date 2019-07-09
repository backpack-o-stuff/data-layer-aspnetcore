using DL.Application.Domain.Scoreboards;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Scoreboards
{
    public class ScoreboardEntryModelBuilder
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ScoreboardEntry>();
            entity.ToTable("scoreboard_entries");

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("INTEGER")
                .IsRequired();
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ScoreboardId)
                .HasColumnName("scoreboard_id")
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(x => x.MonsterId)
                .HasColumnName("monster_id")
                .HasColumnType("INTEGER")
                .IsRequired();

            entity.Property(x => x.PlayersDefeated)
                .HasColumnName("players_defeated")
                .HasColumnType("INTEGER")
                .IsRequired();
        }
    }
}