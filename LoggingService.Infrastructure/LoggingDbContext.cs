using LoggingService.Core.Models;
using LoggingService.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace LoggingService
{
    public class LoggingDbContext : DbContext
    {
        public DbSet<Application> Application { get; set; }
        public DbSet<LogMessage> LogMessage { get; set; }

        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>().HasIndex(p => new { p.Name });
            modelBuilder.HasPostgresEnum<LogLevel>();

            modelBuilder.Entity<Application>().Property("Id").UseSerialColumn();
            modelBuilder.Entity<Application>().Property("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Application>().HasMany<LogMessage>(nameof(Core.Models.Application.LogMessages));

            modelBuilder.Entity<LogMessage>().Property("Id").UseSerialColumn();
            modelBuilder.Entity<LogMessage>().Property("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<LogMessage>().HasKey(key => key.Id);
            modelBuilder.Entity<LogMessage>().HasOne<Application>(nameof(Core.Models.LogMessage.Application));
        }
    }
}