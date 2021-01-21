using System.Collections.Generic;
using System.Linq;
using Serilog;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.Seeders
{
    public static class PermissionsSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, ILogger logger)
        {
            var newCount = 0;
            var userManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "User Management");
            var roleManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "Role Management");


            var permissions = new List<Permission>
            {
                new Permission
                    {Name = "user_add", DisplayName = "Create User", Module = userManagement},
                new Permission {Name = "user_edit", DisplayName = "Edit User", Module = userManagement},
                new Permission {Name = "user_view", DisplayName = "View User", Module = userManagement},
                new Permission {Name = "user_delete", DisplayName = "Delete User", Module = userManagement},
                new Permission {Name = "role_add", DisplayName = "Create Role", Module = roleManagement},
                new Permission {Name = "role_edit", DisplayName = "Edit Role", Module = roleManagement},
                new Permission {Name = "role_view", DisplayName = "View Role", Module = roleManagement},
                new Permission {Name = "role_delete", DisplayName = "Delete Role", Module = roleManagement},
                new Permission
                    {Name = "role_permission_view", DisplayName = "View Role Permissions", Module = roleManagement}
            }.ToList();

            permissions.ForEach(permission =>
            {
                if (!dbContext.Permissions.Any(p => p.Name == permission.Name))
                {
                    newCount++;
                    logger.Information($"Seeding permission => {permission.Name}");
                    dbContext.Permissions.Add(permission);
                }
            });
            dbContext.SaveChanges();
            logger.Information($"********************************************************");
            logger.Information($"*             {newCount} permissions added                      *");
            logger.Information($"********************************************************");
        }
    }
}
