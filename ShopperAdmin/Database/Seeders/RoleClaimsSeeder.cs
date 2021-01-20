using System;
using System.Linq;
using Serilog;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public class RoleClaimsSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            var adminRole = dbContext.Roles.SingleOrDefault(role => role.Name == "Administrator");

            if (adminRole == null)
            {
                logger.Error($"Role does not exist");
                throw new NullReferenceException("Role can not be null");
            }

            var permissions = dbContext.Permissions.ToList();

            permissions.ForEach(perm =>
            {
                if (!dbContext.RoleClaims
                    .Any(claim => claim.ClaimType == "permission"
                                  & claim.ClaimValue == perm.Name
                                  & claim.RoleId == adminRole.Id))
                {
                    var roleClaim = new RoleClaim
                        {RoleId = adminRole.Id, ClaimType = "permission", ClaimValue = perm.Name};
                    logger.Information($"Adding {roleClaim.ClaimValue} claim to {adminRole.Name} role");
                    dbContext.RoleClaims.Add(roleClaim);
                }
            });

            dbContext.SaveChanges();
        }
    }
}