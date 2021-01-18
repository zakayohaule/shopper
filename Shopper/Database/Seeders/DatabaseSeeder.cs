using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
{
    public class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext dbContext,
            AdminAppDbContext adminAppDbContext,
            IPasswordHasher<AppUser> passwordHasher,
            UserManager<AppUser> userManager,
            ILogger logger,
            IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            ModulesSeeder.Seed(dbContext, logger);
            PermissionsSeeder.Seed(dbContext, logger);
            TenantSeeder.Seed(adminAppDbContext,configuration, logger);
            TenantSeeder.Seed(dbContext,adminAppDbContext, logger);
            RoleSeeder.Seed(serviceProvider, dbContext, logger);
            UsersSeeder.Seed(serviceProvider, dbContext,passwordHasher, userManager, logger);

        }

        public static void FixWhenAfterMigrating(ApplicationDbContext dbContext, ILogger logger)
        {
            var sales = dbContext.Sales.IgnoreQueryFilters().Include(s => s.Sku).ToList();
            foreach (var sale in sales.Where(sale => sale.ProductId == null))
            {
                logger.Information($"********** adding product id in sale {sale.Id}");
                sale.ProductId = sale.Sku?.ProductId;
            }

            dbContext.SaveChanges();
        }
    }
}
