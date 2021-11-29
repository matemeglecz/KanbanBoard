using Microsoft.EntityFrameworkCore;
using System;

namespace KanbanBoardApi.Data
{
    public class KanbanBoardContext : DbContext
    {
        public KanbanBoardContext(DbContextOptions<KanbanBoardContext> options)
            : base(options)
        {
        }

        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Lane>().ToTable("Lane")
                .HasMany(b => b.Cards)
                .WithOne(c => c.Lane);

            modelBuilder.Entity<Card>().ToTable("Card")
                .Property(c => c.Description)
                .HasDefaultValue("");

        }
    }
}
