using System.Collections.Generic;
using System.Linq;
using Serilog;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public class RoleSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            logger.Information("*********** SEEDING ROLES ****************");
            var roles = new List<Role>
            {
                new Role {Name = "Administrator", NormalizedName = "ADMINISTRATOR", DisplayName = "Administrator"}
            }.ToList();

            roles.ForEach(role =>
            {
                if (!dbContext.Roles.Any(role1 => role1.Name == role.Name))
                {
                    logger.Information($"Seeding role => {role.Name}");
                    dbContext.Add(role);
                }
            });

            dbContext.SaveChanges();
        }
    }
}
