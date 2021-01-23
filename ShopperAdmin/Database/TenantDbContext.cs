using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.Entities.Identity;
using ShopperAdmin.Mvc.Entities.Tenants;

namespace ShopperAdmin.Database
{
    public class TenantDbContext : DbContext
    {
        public string ConnectionString;

        public TenantDbContext([NotNull] DbContextOptions<TenantDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(ConnectionString);
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> Users { get; set; }
        public DbSet<TenantRole> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
