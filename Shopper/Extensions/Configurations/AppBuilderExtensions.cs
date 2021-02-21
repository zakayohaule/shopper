﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Serilog;
using Shopper.Database;
using Shopper.Database.Seeders;
using Shopper.Mvc.Entities.Identity;
using Shopper.Services;

namespace Shopper.Extensions.Configurations
{
    public static class AppBuilderExtensions
    {
        public static IHost SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.GetService<ApplicationDbContext>();
                var adminAppDbContext = scope.GetService<AdminAppDbContext>();
                var passwordHasher = scope.GetService<IPasswordHasher<AppUser>>();
                var userManager = scope.GetService<UserManager<AppUser>>();
                var logger = scope.GetService<ILogger>();
                var serviceProvider = scope.GetService<IServiceProvider>();
                var configuration = host.GetService<IConfiguration>();

                var seedDatabase = configuration.GetSection("Database").GetValue<bool?>("Seed") ?? false;

                if (seedDatabase)
                {
                    logger.Information("********** Seeding database *************");
                    DatabaseSeeder.Seed(dbContext, adminAppDbContext, passwordHasher, userManager, logger,
                        serviceProvider);
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

        public static IApplicationBuilder UseTenantResolver(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }

        public static IApplicationBuilder UseLocalization(
            this IApplicationBuilder builder)
        {
            var supportedCultures = new[] {"en", "sw"};
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            return builder.UseRequestLocalization(localizationOptions);
        }

        public static IHost CacheTranslations(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var memoryCache = scope.GetService<IMemoryCache>();
                var env = scope.GetService<IWebHostEnvironment>();
                Console.WriteLine("**************** Start Caching Translations ********************8");
                memoryCache.Set("sw", ReadLanguageFile(env, "sw"), new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddYears(5)));
                Console.WriteLine("**************** End Caching Translations ********************8");
                var swTrans = memoryCache.Get<Dictionary<string, string>>("sw");

                Console.WriteLine($"{swTrans}");
            }

            return host;
        }

        public static Dictionary<string, string> ReadLanguageFile(IWebHostEnvironment environment, string lang = null)
        {
            var contentRoot = environment.ContentRootPath;
            var filePath = $"{contentRoot}/Translations/{lang}.json";
            var trans  = JsonConvert
                .DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filePath));
            // Console.WriteLine($"***************** Caching sw Translations *****************");
            // foreach (var keyValuePair in trans)
            // {
            //     Console.WriteLine($"***************** {keyValuePair.Key} => {keyValuePair.Value} *****************");
            // }

            return trans;
        }

        public static IEnumerable<LocalizedString> GetLocalizedStringsFromLanguageFiles(IWebHostEnvironment environment, string lang = null)
        {
            var contentRoot = environment.ContentRootPath;
            var filePath = $"{contentRoot}/Translations/{lang}.json";
            var trans  = JsonConvert
                .DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filePath));
            Console.WriteLine($"***************** Caching sw Translations *****************");
            foreach (var keyValuePair in trans)
            {
                Console.WriteLine($"***************** {keyValuePair.Key} => {keyValuePair.Value} *****************");
            }

            return trans.Select(s => new LocalizedString(s.Key, s.Value));
        }
    }
}
