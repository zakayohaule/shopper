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
            logger.Information("*********** SEEDING PERMISSIONS ****************");
            var newCount = 0;
            var userManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "User Management");
            var roleManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "Role Management");

            var databaseManagement = dbContext
                .Modules
                .SingleOrDefault(g => g.Name == "Database Management");

            var tenantManagement = dbContext
                .Modules
                .SingleOrDefault(g => g.Name == "Tenant Management");


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
                new Permission {Name = "role_permission_view", DisplayName = "View Role Permissions", Module = roleManagement},
                new Permission {Name = "role_permissions_save", DisplayName = "Save A Role's Permissions", Module = roleManagement},
                new Permission {Name = "database_add", DisplayName = "Create Database", Module = databaseManagement},
                new Permission {Name = "database_edit", DisplayName = "Edit Database", Module = databaseManagement},
                new Permission {Name = "database_view", DisplayName = "View Database", Module = databaseManagement},
                new Permission {Name = "database_delete", DisplayName = "Delete Database", Module = databaseManagement},
                new Permission {Name = "tenant_add", DisplayName = "Create Tenant", Module =tenantManagement},
                new Permission {Name = "tenant_edit", DisplayName = "Edit Tenant", Module =tenantManagement},
                new Permission {Name = "tenant_view", DisplayName = "View Tenant", Module =tenantManagement},
                new Permission {Name = "tenant_delete", DisplayName = "Delete Tenant", Module =tenantManagement},
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
