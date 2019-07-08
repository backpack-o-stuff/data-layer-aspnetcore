using DL.Application.Domain.Monsters;
using Microsoft.EntityFrameworkCore;

namespace DL.Data.Monsters
{
    public static class MonsterModelBuilder
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Monster>();
            entity.ToTable("monsters");

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("INTEGER")
                .IsRequired();
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(30)")
                .IsRequired();

            entity.Property(x => x.Power)
                .HasColumnName("power")
                .HasColumnType("INTEGER")
                .IsRequired();
        }
    }
}