using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database
{
    public class ApplicationDbContext : IdentityDbContext<
        AppUser, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Identity entities
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<AppUser> Users { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        //custom entities
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Module> Modules { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
