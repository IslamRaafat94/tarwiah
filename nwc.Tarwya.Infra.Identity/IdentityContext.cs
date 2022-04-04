using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace nwc.Tarwya.Infra.Identity
{
    public class IdentityContext : IdentityDbContext<
                                        IdentityUser<long>,
                                        IdentityRole<long>,
                                        long,
                                        IdentityUserClaim<long>,
                                        IdentityUserRole<long>,
                                        IdentityUserLogin<long>,
                                        IdentityRoleClaim<long>,
                                        IdentityUserToken<long>>
    {
        public IdentityContext()

        {
        }
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Tarwya;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<IdentityUser<long>>(entity =>
        {
            entity.ToTable(schema: "Security", name: "Users");
        });

            builder.Entity<IdentityRole<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "Roles");
            });
            builder.Entity<IdentityUserRole<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "UserRoles");
                //in case you chagned the TKey type
                entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            builder.Entity<IdentityUserClaim<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "UserClaims");

            });

            builder.Entity<IdentityUserLogin<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "UserLogins");
                //in case you chagned the TKey type
                entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });

            builder.Entity<IdentityRoleClaim<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "RoleClaims");
            });

            builder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable(schema: "Security", name: "UserTokens");
                //in case you chagned the TKey type
                entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

            });
        }
    }
}
