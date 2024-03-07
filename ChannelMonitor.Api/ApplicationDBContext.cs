using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        private readonly Guid? _tenantId;

        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options,
            ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Channel>()
                .HasQueryFilter(o => o.TenantId == _tenantId);

            modelBuilder.Entity<ChannelOrigin>()
                .HasQueryFilter(o => o.TenantId == _tenantId);

            modelBuilder.Entity<ChannelDetail>()
                .HasQueryFilter(o => o.TenantId == _tenantId);

            modelBuilder.Entity<FailureLogging>()
                .HasQueryFilter(o => o.TenantId == _tenantId);

            modelBuilder.Entity<Worker>()
                .HasQueryFilter(o => o.TenantId == _tenantId);

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

            modelBuilder.Entity<AlertStatus>().HasData(
                new AlertStatus { Id = 1, Name = "Ok", Color = "green", Emoji = null },
                new AlertStatus { Id = 2, Name = "Alert", Color = "yellow", Emoji = null },
                new AlertStatus { Id = 3, Name = "Fail", Color = "red", Emoji = null },
                new AlertStatus { Id = 4, Name = "Pause", Color = "grey", Emoji = null }
            );

            modelBuilder.Entity<FailureType>().HasData(
                new FailureType { Id = 1, Name = "Audio" },
                new FailureType { Id = 2, Name = "Video" },
                new FailureType { Id = 3, Name = "General" }
            );

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = new Guid("ec576c36-9da4-4d2c-821e-7888f0b4e8a9"), Name = "General" }
            );

            // Seteamos nombres personalizados a las tablas de roles.
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuariosClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuariosLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuariosRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuariosTokens");

        }

        public DbSet<AlertStatus> AlertStatus { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelDetail> ChannelDetails { get; set; }
        public DbSet<ChannelOrigin> ChannelOrigins { get; set; }
        public DbSet<FailureLogging> FailureLoggings { get; set; }
        public DbSet<FailureType> FailureTypes { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Worker> Workers { get; set; }

    }
}
