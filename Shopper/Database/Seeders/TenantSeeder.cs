using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shopper.Mvc.Entities;
using Shopper.Mvc.Enums;

namespace Shopper.Database.Seeders
{
    public class TenantSeeder
    {
        public static void Seed(AdminAppDbContext adminDbContext,IConfiguration configuration, ILogger logger)
        {
            var defaultConnectString = configuration.GetConnectionString("Default").Replace("{dbName}", "shopper");
            var tenants = new List<Tenant>
            {
                new Tenant
                {
                    Active = true,
                    Code = "KEA",
                    Address = "Sinza Madukani",
                    Description = "Baby Shop",
                    Domain = $"kea",
                    Email = "kea@kea.com",
                    Name = "Kea Baby Shop",
                    ConnectionString =
                        defaultConnectString,
                    SubscriptionType = SubscriptionType.Annual,
                },
                new Tenant
                {
                    Active = true,
                    Code = "LCH",
                    Address = "Localhost",
                    Description = "Localhost shop",
                    Domain = $"zaks",
                    Email = "admin@shopper.com",
                    Name = "Localhost Shop",
                    ConnectionString =
                        defaultConnectString,
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
