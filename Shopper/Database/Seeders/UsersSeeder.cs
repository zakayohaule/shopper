using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
{
    public class UsersSeeder
    {
        public static void Seed(IServiceProvider serviceProvider, ApplicationDbContext dbContext, IPasswordHasher<AppUser> passwordHasher,
            UserManager<AppUser> userManager, ILogger logger)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var keaTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == $"kea");
            var localhostTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain == $"zaks");
            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "keaadmin",
                    FullName = "Ella Maira",
                    Email = "ellygabbymaira@gmail.com",
                    EmailConfirmed = true,
                    HasResetPassword = true,
                    Tenant = keaTenant,
                },
                new AppUser
                {
                    UserName = "localhostadmin",
                    FullName = "Admin Localhost",
                    Email = "admin@localhost.com",
                    EmailConfirmed = true,
                    HasResetPassword = true,
                    Tenant = localhostTenant,
                }
            };

            foreach (var aUser in users)
            {
                if (!dbContext.Users.IgnoreQueryFilters().Any(u => u.Email.Equals(aUser.Email)))
                {

                    dbContext.Tenant = aUser.Tenant;
                    var result = userManager.CreateAsync(aUser, "123456").Result;
                    if (!result.Succeeded)
                    {
                        logger.Error($"Could not create user => {aUser.Email}");
                        return;
                    }

                    logger.Information($"Seeding user => {aUser.Email}");
                    dbContext.SaveChanges();
                }
                else
                {
                    continue;
                }

                var adminUser = dbContext.Users
                    .IgnoreQueryFilters()
                    .FirstOrDefault(u => u.Email.Equals(aUser.Email));

                var adminRole = dbContext.Roles
                    .IgnoreQueryFilters()
                    .FirstOrDefault(r => r.Name.Equals("Administrator") && r.TenantId.Equals(aUser.TenantId));

                if (adminUser == null || adminRole == null)
                {
                    logger.Error("Role or user not found");
                    throw new NullReferenceException("Role or User is null");
                }

                if (dbContext.UserRoles.IgnoreQueryFilters().Any(role => role.RoleId == adminRole.Id & role.UserId == adminUser.Id))
                    continue;
                var userRole = new UserRole
                {
                    Role = adminRole,
                    User = adminUser
                };

                logger.Information($"Seeding user role {userRole.User.Email} => {adminRole.Name}");
                dbContext.UserRoles.Add(userRole);
                dbContext.SaveChanges();
            }
        }
    }
}
