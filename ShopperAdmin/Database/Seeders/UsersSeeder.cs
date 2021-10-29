using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public class UsersSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, UserManager<AppUser> userManager, ILogger logger)
        {
            logger.Information("*********** SEEDING USERS ****************");
            // var eGa = dbContext.Institutions.First(institution => institution.VoteCode == "TR97");
            var user = new AppUser
            {
                UserName = "admin",
                FullName = "Admin Admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                HasResetPassword = true,
                // Institution = eGa
            };

            if (!dbContext.Users.Any(appUser => appUser.Email == "admin@admin.com"))
            {
                var result = userManager.CreateAsync(user, "123456").Result;
                if (!result.Succeeded)
                {
                    logger.Error($"Could not create user => {user.Email}");
                    return;
                }

                logger.Information($"Seeding user => {user.Email}");
            }
            else
            {
                return;
            }

            var adminUser = userManager.FindByEmailAsync("admin@admin.com").Result;

            var adminRole = dbContext.Roles.SingleOrDefault(role => role.Name == "Administrator");

            if (adminUser == null || adminRole == null)
            {
                logger.Error("Role or user not found");
                throw new NullReferenceException("Role or User is null");
            }

            if (!dbContext.UserRoles.Any(role => role.RoleId == adminRole.Id & role.UserId == adminUser.Id))
            {
                var userRole = new UserRole
                {
                    Role = adminRole,
                    User = adminUser
                };

                logger.Information($"Seeding user role {userRole.User.Email} => {adminRole.Name}");
                dbContext.UserRoles.Add(userRole);
            }

            dbContext.SaveChanges();
        }
    }
}
