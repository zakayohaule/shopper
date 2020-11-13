using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopperAdmin.Database;
using ShopperAdmin.Database.Seeders;
using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class AppBuilderExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app,
            ApplicationDbContext dbContext,
            UserManager<AppUser> userManager,
            ILogger logger)
        {
            DatabaseSeeder.Seed(dbContext, userManager, logger);
        }

        public static void UpdateRoleClaims(this IApplicationBuilder app,
            ApplicationDbContext dbContext,
            ILogger logger)
        {
            ModulesSeeder.Seed(dbContext, logger);
            // PermissionsSeeder.Seed(dbContext, logger);
            // RoleClaimsSeeder.Seed(dbContext, logger);
        }

        public static void RefreshDatabase(this IApplicationBuilder app,
            ApplicationDbContext dbContext,
            ILogger logger)
        {
            var entities = dbContext.Model.GetEntityTypes().ToList();
            dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0;");
            entities.ForEach(type =>
            {
                logger.Information($"Truncating table => {type.GetTableName()}");
                // dbContext.Database.ExecuteSqlRaw($"truncate {type.GetTableName()};");
            });
            dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1;");
        }

        public static void ShowBanner()
        {
            var file = new FileStream(@"C:\Users\user\RiderProjects\ShopperAdmin\ShopperAdmin\banner.txt"
                , FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                Console.WriteLine(reader.ReadLine());
            }
        }
    }
}