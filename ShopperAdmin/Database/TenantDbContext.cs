using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext([NotNull] DbContextOptions<TenantDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
