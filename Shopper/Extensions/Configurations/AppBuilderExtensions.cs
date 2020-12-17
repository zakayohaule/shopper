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
using Shopper.Services;

namespace Shopper.Extensions.Configurations
{
    public static class AppBuilderExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app,
            ApplicationDbContext dbContext,
            AdminAppDbContext adminAppDbContext,
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher,
            ILogger logger,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            DatabaseSeeder.Seed(dbContext,adminAppDbContext,passwordHasher, userManager, logger,serviceProvider);
        }

        public static void UpdateRoleClaims(this IApplicationBuilder app,
            ApplicationDbContext dbContext,
            ILogger logger)
        {
            ModulesSeeder.Seed(dbContext, logger);
            PermissionsSeeder.Seed(dbContext, logger);
            RoleClaimsSeeder.Seed(dbContext, logger);
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

        public static IApplicationBuilder UseTenantResolver(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
