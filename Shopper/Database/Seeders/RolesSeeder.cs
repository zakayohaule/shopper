using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
{
    public class RoleSeeder
    {
        public static void Seed(IServiceProvider serviceProvider, ApplicationDbContext dbContext, ILogger logger)
        {
            logger.Information("************ ROLE SEEDER ***************");
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var defaultSubdomain = configuration.GetValue<string>("DefaultSubdomain");
            var keaTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == $"kea");
            var localhostTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == $"zaks");
            if (keaTenant != null && localhostTenant != null)
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Administrator",
                        NormalizedName = $"KEA_ADMINISTRATOR",
                        DisplayName = "Administrator",
                        Tenant = keaTenant
                    },
                    new Role
                    {
                        Name = "Administrator",
                        NormalizedName = $"LOCALHOST_ADMINISTRATOR",
                        DisplayName = "Administrator",
                        Tenant = localhostTenant
                    }
                }.ToList();

                foreach (var role in roles)
                {
                    if (!dbContext.Roles.IgnoreQueryFilters().Any(r => r.NormalizedName.Equals(role.NormalizedName)))
                    {
                        logger.Information($"Seeding role => {role.Name}");
                        dbContext.Add(role);
                        dbContext.Tenant = role.Tenant;
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
