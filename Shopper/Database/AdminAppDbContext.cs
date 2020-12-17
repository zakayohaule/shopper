using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;

namespace Shopper.Database
{
    public class AdminAppDbContext : DbContext
    {
        public AdminAppDbContext([NotNull] DbContextOptions<AdminAppDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
