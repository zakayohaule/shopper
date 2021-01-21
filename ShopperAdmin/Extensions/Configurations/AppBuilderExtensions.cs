using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ShopperAdmin.Database;
using ShopperAdmin.Database.Seeders;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class AppBuilderExtensions
    {
        public static IHost SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.GetService<ApplicationDbContext>();
                var userManager = scope.GetService<UserManager<AppUser>>();
                var logger = scope.GetService<ILogger>();
                var configuration = host.GetService<IConfiguration>();

                var seedDatabase = configuration.GetSection("Database").GetValue<bool?>("Seed") ?? false;
                if (seedDatabase)
                {
                    logger.Information("********** Seeding database *************");
                    DatabaseSeeder.Seed(dbContext,userManager, logger);
                }
                ModulesSeeder.Seed(dbContext, logger);
                PermissionsSeeder.Seed(dbContext, logger);
                RoleClaimsSeeder.Seed(dbContext, logger);
            }

            return host;
        }

        public static T GetService<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        public static T GetService<T>(this IHost host)
        {
            return host.Services.GetService<T>();
        }

        public static void ShowBanner()
        {
            var file = new FileStream(@"C:\Users\user\RiderProjects\Shopper\Shopper\banner.txt"
                , FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

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

        /*public static IApplicationBuilder UseTenantResolver(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }*/
    }
}
