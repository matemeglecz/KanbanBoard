using KanbanBoardApi.Models;
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

        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>().ToTable("Board")
                .HasMany(b => b.Cards)
                .WithOne(c => c.Board);

            modelBuilder.Entity<Card>().ToTable("Card")
                .Property(c => c.Description)
                .HasDefaultValue("");

        }
    }
}
