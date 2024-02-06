using ChannelMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Channel>()
                .HasOne(c => c.VideoFailure)
                .WithMany()
                .HasForeignKey(c => c.VideoFailureId)
                .OnDelete(DeleteBehavior.Restrict);
                // RESTRICT: No permite la eliminación de la fila en la tabla principal
                // si hay filas correspondientes en la tabla secundaria.

            modelBuilder.Entity<Channel>()
                .HasOne(c => c.AudioFailure)
                .WithMany()
                .HasForeignKey(c => c.AudioFailureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Channel>()
                .HasOne(c => c.GeneralFailure)
                .WithMany()
                .HasForeignKey(c => c.GeneralFailureId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<AlertStatus> AlertStatus { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelDetail> ChannelDetails { get; set; }
        public DbSet<ChannelOrigin> ChannelOrigins { get; set; }
        public DbSet<FailureLogging> FailureLoggings { get; set; }
        public DbSet<FailureType> FailureTypes { get; set; }
        public DbSet<Error> Errors { get; set; }

    }
}
