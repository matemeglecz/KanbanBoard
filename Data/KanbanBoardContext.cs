using KanbanBoardApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Lane>().ToTable("Lane")
                .HasMany(b => b.Cards)
                .WithOne(c => c.Lane);

            modelBuilder.Entity<Card>().ToTable("Card")
                .Property(c => c.Description)
                .HasDefaultValue("");

        }
    }
}
