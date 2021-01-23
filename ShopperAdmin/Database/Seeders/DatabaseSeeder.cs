using Microsoft.AspNetCore.Identity;
using Serilog;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, UserManager<AppUser> userManager, ILogger logger)
        {
            ModulesSeeder.Seed(dbContext, logger);
            PermissionsSeeder.Seed(dbContext, logger);
            RoleSeeder.Seed(dbContext, logger);
            UsersSeeder.Seed(dbContext, userManager, logger);
        }
    }
}
