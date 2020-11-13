using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Mvc.Entities;
using Serilog;
using Shared.Mvc.Entities.Identity;

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
            var institutionManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "Institution Management");
            var roleManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "Role Management");

            var softwareManagement = dbContext
                .Modules
                .SingleOrDefault(group => group.Name == "Software Management");

            var permissions = new List<Permission>
            {
                new Permission
                    {Name = "user_add", DisplayName = "Create Own Institution's User", Module = userManagement},
                new Permission {Name = "user_add_any", DisplayName = "Create Any User", Module = userManagement},
                new Permission {Name = "user_edit", DisplayName = "Edit User", Module = userManagement},
                new Permission {Name = "user_edit_any", DisplayName = "Edit Any User", Module = userManagement},
                new Permission {Name = "user_view", DisplayName = "View User", Module = userManagement},
                new Permission {Name = "user_delete", DisplayName = "Delete User", Module = userManagement},
                new Permission
                    {Name = "institution_add", DisplayName = "Create Institution", Module = institutionManagement},
                new Permission
                    {Name = "institution_edit", DisplayName = "Edit Institution", Module = institutionManagement},
                new Permission
                    {Name = "institution_view", DisplayName = "View Institution", Module = institutionManagement},
                new Permission
                    {Name = "institution_delete", DisplayName = "Delete Institution", Module = institutionManagement},
                new Permission {Name = "role_add", DisplayName = "Create Role", Module = roleManagement},
                new Permission {Name = "role_edit", DisplayName = "Edit Role", Module = roleManagement},
                new Permission {Name = "role_view", DisplayName = "View Role", Module = roleManagement},
                new Permission {Name = "role_delete", DisplayName = "Delete Role", Module = roleManagement},
                new Permission
                    {Name = "role_permission_view", DisplayName = "View Role Permissions", Module = roleManagement},
                new Permission {Name = "software_add", DisplayName = "Create Software", Module = softwareManagement},
                new Permission {Name = "software_edit", DisplayName = "Edit Software", Module = softwareManagement},
                new Permission {Name = "software_view", DisplayName = "View Software", Module = softwareManagement},
                new Permission {Name = "software_delete", DisplayName = "Delete Software", Module = softwareManagement},
                new Permission
                {
                    Name = "software_add_endpoint", DisplayName = "Add Software Endpoint", Module = softwareManagement
                },
                new Permission
                {
                    Name = "software_edit_endpoint", DisplayName = "Edit Software Endpoint", Module = softwareManagement
                },
                new Permission
                {
                    Name = "software_delete_endpoint", DisplayName = "Delete Software Endpoint",
                    Module = softwareManagement
                },
                new Permission
                {
                    Name = "connection_request_received", DisplayName = "View Received Connection Requests",
                    Module = softwareManagement
                },
                new Permission
                {
                    Name = "connection_request_sent", DisplayName = "View Sent Connection Requests",
                    Module = softwareManagement
                },
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