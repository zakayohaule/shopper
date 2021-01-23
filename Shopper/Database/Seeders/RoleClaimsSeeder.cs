using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
{
    public class RoleClaimsSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            var keaAdminRole = dbContext.Roles.IgnoreQueryFilters().SingleOrDefault(role => role.NormalizedName == "KEA_ADMINISTRATOR");
            var localhostAdminRole = dbContext.Roles.IgnoreQueryFilters().SingleOrDefault(role => role.NormalizedName == "LOCALHOST_ADMINISTRATOR");
            UpdateRoleClaims(keaAdminRole, logger, dbContext);
            UpdateRoleClaims(localhostAdminRole, logger, dbContext);
            dbContext.SaveChanges();
        }

        public static void UpdateRoleClaims(Role role, ILogger logger, ApplicationDbContext dbContext)
        {
            if (role == null)
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
                                  & claim.RoleId == role.Id))
                {
                    var roleClaim = new RoleClaim
                        {RoleId = role.Id, ClaimType = "permission", ClaimValue = perm.Name};
                    logger.Information($"Adding {roleClaim.ClaimValue} claim to {role.NormalizedName} role");
                    dbContext.RoleClaims.Add(roleClaim);
                }
            });

        }
    }
}
