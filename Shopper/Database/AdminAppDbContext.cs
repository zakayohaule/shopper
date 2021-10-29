using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Shopper.Mvc.Entities;

namespace Shopper.Database
{
    public class AdminAppDbContext : DbContext
    {
        public AdminAppDbContext([NotNull] DbContextOptions<AdminAppDbContext> options) : base(options)
        {
            Console.WriteLine("Getting AdminAppDbContext with parameters");
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
