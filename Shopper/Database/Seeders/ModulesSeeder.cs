﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Mvc.Entities;
using Serilog;
 using Shared.Mvc.Entities.Identity;

 namespace Shopper.Database.Seeders
{
    public static class ModulesSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            var modules = new List<Module>
            {
                new Module {Name = "User Management"},
                new Module {Name = "Role Management"},
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
