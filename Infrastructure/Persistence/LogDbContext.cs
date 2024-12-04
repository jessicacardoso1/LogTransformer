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
            modelBuilder.Entity<LogEntry>().HasKey(l => l.Id);
            modelBuilder.Entity<TransformedLog>().HasKey(t => t.Id);

            modelBuilder.Entity<TransformedLog>()
                .HasOne(t => t.OriginalLog)
                .WithMany()
                .HasForeignKey(t => t.OriginalLogId);
        }
    }

}
