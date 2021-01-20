using System.Collections.Generic;
using System.Linq;
using Serilog;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public static class ModulesSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            var modules = new List<Module>
            {
                new Module {Name = "User Management"},
                new Module {Name = "Institution Management"},
                new Module {Name = "Role Management"},
                new Module {Name = "Software Management"},
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
