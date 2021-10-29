using System.Collections.Generic;
using System.Linq;
using Serilog;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
{
    public static class ModulesSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            logger.Information("********* SEEDING MODULES *****************");
            var modules = new List<Module>
            {
                new Module {Name = "User Management"},
                new Module {Name = "Role Management"},
                new Module {Name = "Product Group Management"},
                new Module {Name = "Product Category Management"},
                new Module {Name = "Product Type Management"},
                new Module {Name = "Attribute Management"},
                new Module {Name = "Price Type Management"},
                new Module {Name = "Product Management"},
                new Module {Name = "Stock Management"},
                new Module {Name = "Sale Management"},
                new Module {Name = "Expenditure Management"},
                new Module {Name = "Business Info Management"},
                new Module {Name = "Dashboard"},
                new Module {Name = "Reports"},
            }.ToList();

            modules.ForEach(pg =>
            {
                if (!dbContext.Modules.Any(g => g.Name == pg.Name))
                {
                    logger.Information($"Seeding module => {pg.Name}");
                    dbContext.Modules.Add(pg);
                }
            });

            dbContext.SaveChanges();
        }
    }
}
