using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Serilog;
using Shared.Mvc.Entities.Identity;

namespace Shopper.Database.Seeders
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

            var productGroupManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Product Group Management"));

            var productCategoryManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Product Category Management"));

            var attributeManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Attribute Management"));

            var priceTypeManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Price Type Management"));

            var productManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Product Management"));

            var stockManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Stock Management"));
            var saleManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Sale Management"));

            var permissions = new List<Permission>
            {
                new Permission {Name = "user_add", DisplayName = "Create Own Institution's User", Module = userManagement},
                new Permission {Name = "user_add_any", DisplayName = "Create Any User", Module = userManagement},
                new Permission {Name = "user_edit", DisplayName = "Edit User", Module = userManagement},
                new Permission {Name = "user_edit_any", DisplayName = "Edit Any User", Module = userManagement},
                new Permission {Name = "user_view", DisplayName = "View User", Module = userManagement},
                new Permission {Name = "user_delete", DisplayName = "Delete User", Module = userManagement},
                new Permission {Name = "role_add", DisplayName = "Create Role", Module = roleManagement},
                new Permission {Name = "role_edit", DisplayName = "Edit Role", Module = roleManagement},
                new Permission {Name = "role_view", DisplayName = "View Role", Module = roleManagement},
                new Permission {Name = "role_delete", DisplayName = "Delete Role", Module = roleManagement},
                new Permission {Name = "role_permission_view", DisplayName = "View Role Permissions", Module = roleManagement},
                new Permission {Name = "product_category_add", DisplayName = "Add Product Categories", Module = productCategoryManagement},
                new Permission {Name = "product_category_view", DisplayName = "View Product Categories", Module = productCategoryManagement},
                new Permission {Name = "product_category_edit", DisplayName = "Edit Product Categories", Module = productCategoryManagement},
                new Permission {Name = "product_category_delete", DisplayName = "Delete Product Categories", Module = productCategoryManagement},
                new Permission {Name = "attribute_add", DisplayName = "Add Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_view", DisplayName = "View Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_edit", DisplayName = "Edit Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_delete", DisplayName = "Delete Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_option_add", DisplayName = "Add Attribute options", Module = attributeManagement},
                new Permission {Name = "attribute_option_view", DisplayName = "View Attribute options", Module = attributeManagement},
                new Permission {Name = "attribute_option_edit", DisplayName = "Edit Attribute options", Module = attributeManagement},
                new Permission {Name = "attribute_option_delete", DisplayName = "Delete Attribute options", Module = attributeManagement},
                new Permission {Name = "price_type_add", DisplayName = "Add Price Types", Module = priceTypeManagement},
                new Permission {Name = "price_type_view", DisplayName = "View Price Types", Module = priceTypeManagement},
                new Permission {Name = "price_type_edit", DisplayName = "Edit Price Types", Module = priceTypeManagement},
                new Permission {Name = "price_type_delete", DisplayName = "Delete Price Types", Module = priceTypeManagement},
                new Permission {Name = "product_group_add", DisplayName = "Add Product Groups", Module = productGroupManagement},
                new Permission {Name = "product_group_view", DisplayName = "View Product Groups", Module = productGroupManagement},
                new Permission {Name = "product_group_edit", DisplayName = "Edit Product Groups", Module = productGroupManagement},
                new Permission {Name = "product_group_delete", DisplayName = "Delete Product Groups", Module = productGroupManagement},
                new Permission {Name = "product_add", DisplayName = "Add Products", Module = productManagement},
                new Permission {Name = "product_view", DisplayName = "View Products", Module = productManagement},
                new Permission {Name = "product_edit", DisplayName = "Edit Products", Module = productManagement},
                new Permission {Name = "product_delete", DisplayName = "Delete Products", Module = productManagement},
                new Permission {Name = "stock_add", DisplayName = "Add Stock", Module = stockManagement},
                new Permission {Name = "stock_view", DisplayName = "View Stock", Module = stockManagement},
                new Permission {Name = "stock_edit", DisplayName = "Edit Stock", Module = stockManagement},
                new Permission {Name = "stock_delete", DisplayName = "Delete Stock", Module = stockManagement},
                new Permission {Name = "sale_record", DisplayName = "Record Sale", Module = saleManagement},
                new Permission {Name = "sale_view", DisplayName = "View Sales", Module = saleManagement},
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
            if (newCount>0)
            {
                logger.Information($"********************************************************");
                logger.Information($"*             {newCount} permissions added                      *");
                logger.Information($"********************************************************");
            }
        }
    }
}
