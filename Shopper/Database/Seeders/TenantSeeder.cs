using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.Enums;

namespace Shopper.Database.Seeders
{
    public class TenantSeeder
    {
        public static void Seed(AdminAppDbContext adminDbContext, ILogger logger)
        {
            var tenants = new List<Tenant>
            {
                new Tenant
                {
                    Active = true,
                    Address = "Sinza Madukani",
                    Description = "Baby Shop",
                    Domain = "kea.localhost",
                    Email = "kea@kea.com",
                    Name = "Kea Baby Shop",
                    ConnectionString =
                        "Server=localhost; Port=3306; Database=shopper; Uid=root; Allow User Variables=true",
                    SubscriptionType = SubscriptionType.Annual,
                },
                new Tenant
                {
                    Active = true,
                    Address = "Localhost",
                    Description = "Localhost shop",
                    Domain = "localhost",
                    Email = "admin@shopper.com",
                    Name = "Localhost Shop",
                    ConnectionString =
                        "Server=localhost; Port=3306; Database=shopper; Uid=root; Allow User Variables=true",
                    SubscriptionType = SubscriptionType.Annual,
                },
            };

            tenants.ForEach(tenant =>
            {
                if (!adminDbContext.Tenants.Any(tnt => tnt.Domain == tenant.Domain))
                {
                    logger.Information($"Seeding tenant in admin database => {tenant.Name}");
                    adminDbContext.Add(tenant);
                }
            });

            adminDbContext.SaveChanges();
        }

        public static void Seed(ApplicationDbContext dbContext,AdminAppDbContext adminAppDbContext, ILogger logger)
        {
            var tenants = adminAppDbContext.Tenants.ToList();
            tenants.ForEach(tenant =>
            {
                if (!dbContext.Tenants.Any(tnt => tnt.Domain == tenant.Domain))
                {
                    logger.Information($"Seeding tenant in shopper database => {tenant.Name}");
                    dbContext.Add(tenant);
                }
            });

            dbContext.SaveChanges();
        }
    }
}
