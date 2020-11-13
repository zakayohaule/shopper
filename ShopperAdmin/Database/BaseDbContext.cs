using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Database
{
    public class BaseDbContext : IdentityDbContext<
        AppUser, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
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
    }
}