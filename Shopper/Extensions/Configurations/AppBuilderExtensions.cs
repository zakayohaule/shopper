using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Mvc.Entities.Identity;
using Shopper.Database;
using Shopper.Database.Seeders;

namespace Shopper.Extensions.Configurations
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
            PermissionsSeeder.Seed(dbContext, logger);
            RoleClaimsSeeder.Seed(dbContext, logger);
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
            var file = new FileStream(@"C:\Users\user\RiderProjects\Shopper\Shopper\banner.txt"
                ,FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            
            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                Console.WriteLine(reader.ReadLine());
            }
        }

        public static void ShowBannerIfEnabled(this IServiceCollection services, IConfiguration configuration)
        {
            var showBanner = configuration.GetValue<bool?>("ShowBanner") ?? false;
            if (showBanner)
            {
                ShowBanner(); 
            }
        }
    }
}