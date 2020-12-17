using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Mvc.Entities.Identity;
using Shopper.Services.Interfaces;

namespace Shopper.Database.Seeders
{
    public class RoleSeeder
    {
        public static void Seed(IServiceProvider serviceProvider, ApplicationDbContext dbContext, ILogger logger)
        {
            var keaTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == "kea.localhost");
            var localhostTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == "localhost");
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
