using LogTransformer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogTransformer.Infrastructure.Persistence
{
    public class LogDbContext : DbContext
    {
        public DbSet<LogEntry> Logs { get; set; }
        public DbSet<TransformedLog> TransformedLogs { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para LogEntry
            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.OriginalContent)
                      .IsRequired();
            });

            modelBuilder.Entity<TransformedLog>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.TransformedContent)
                      .IsRequired();

                entity.HasOne(t => t.OriginalLog) 
                      .WithMany()
                      .HasForeignKey(t => t.OriginalLogId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
