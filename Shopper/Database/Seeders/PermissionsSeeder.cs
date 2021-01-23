using System.Collections.Generic;
using System.Linq;
using Serilog;
using Shopper.Mvc.Entities.Identity;

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

            var productTypeManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Product Type Management"));

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

            var expenditureManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Expenditure Management"));

            var businessInfoManagement =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Business Info Management"));

            var dashboard =
                dbContext.Modules.SingleOrDefault(module => module.Name.Equals("Dashboard"));

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
                    {Name = "user_assign_role", DisplayName = "Assign A User Role(s)", Module = userManagement},
                new Permission {Name = "role_add", DisplayName = "Create Role", Module = roleManagement},
                new Permission {Name = "role_edit", DisplayName = "Edit Role", Module = roleManagement},
                new Permission {Name = "role_view", DisplayName = "View Role", Module = roleManagement},
                new Permission {Name = "role_delete", DisplayName = "Delete Role", Module = roleManagement},
                new Permission
                {
                    Name = "role_permissions_save", DisplayName = "Save A Role's Permissions", Module = roleManagement
                },
                new Permission
                    {Name = "role_permission_view", DisplayName = "View Role Permissions", Module = roleManagement},
                new Permission
                {
                    Name = "product_category_add", DisplayName = "Add Product Categories",
                    Module = productCategoryManagement
                },
                new Permission
                {
                    Name = "product_category_view", DisplayName = "View Product Categories",
                    Module = productCategoryManagement
                },
                new Permission
                {
                    Name = "product_category_edit", DisplayName = "Edit Product Categories",
                    Module = productCategoryManagement
                },
                new Permission
                {
                    Name = "product_category_delete", DisplayName = "Delete Product Categories",
                    Module = productCategoryManagement
                },
                new Permission
                    {Name = "product_type_add", DisplayName = "Add Product Types", Module = productTypeManagement},
                new Permission
                    {Name = "product_type_view", DisplayName = "View Product Types", Module = productTypeManagement},
                new Permission
                    {Name = "product_type_edit", DisplayName = "Edit Product Types", Module = productTypeManagement},
                new Permission
                {
                    Name = "product_type_delete", DisplayName = "Delete Product Types", Module = productTypeManagement
                },
                new Permission {Name = "attribute_add", DisplayName = "Add Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_view", DisplayName = "View Attributes", Module = attributeManagement},
                new Permission {Name = "attribute_edit", DisplayName = "Edit Attributes", Module = attributeManagement},
                new Permission
                    {Name = "attribute_delete", DisplayName = "Delete Attributes", Module = attributeManagement},
                new Permission
                {
                    Name = "attribute_option_add", DisplayName = "Add Attribute options", Module = attributeManagement
                },
                new Permission
                {
                    Name = "attribute_option_view", DisplayName = "View Attribute options", Module = attributeManagement
                },
                new Permission
                {
                    Name = "attribute_option_edit", DisplayName = "Edit Attribute options", Module = attributeManagement
                },
                new Permission
                {
                    Name = "attribute_option_delete", DisplayName = "Delete Attribute options",
                    Module = attributeManagement
                },
                new Permission {Name = "price_type_add", DisplayName = "Add Price Types", Module = priceTypeManagement},
                new Permission
                    {Name = "price_type_view", DisplayName = "View Price Types", Module = priceTypeManagement},
                new Permission
                    {Name = "price_type_edit", DisplayName = "Edit Price Types", Module = priceTypeManagement},
                new Permission
                    {Name = "price_type_delete", DisplayName = "Delete Price Types", Module = priceTypeManagement},
                new Permission
                    {Name = "product_group_add", DisplayName = "Add Product Groups", Module = productGroupManagement},
                new Permission
                    {Name = "product_group_view", DisplayName = "View Product Groups", Module = productGroupManagement},
                new Permission
                    {Name = "product_group_edit", DisplayName = "Edit Product Groups", Module = productGroupManagement},
                new Permission
                {
                    Name = "product_group_delete", DisplayName = "Delete Product Groups",
                    Module = productGroupManagement
                },
                new Permission
                {
                    Name = "expenditure_type_add", DisplayName = "Add Expenditure Types", Module = expenditureManagement
                },
                new Permission
                {
                    Name = "expenditure_type_view", DisplayName = "View Expenditure Types",
                    Module = expenditureManagement
                },
                new Permission
                {
                    Name = "expenditure_type_edit", DisplayName = "Edit Expenditure Types",
                    Module = expenditureManagement
                },
                new Permission
                {
                    Name = "expenditure_type_delete", DisplayName = "Delete Expenditure Types",
                    Module = expenditureManagement
                },
                new Permission
                    {Name = "expenditure_add", DisplayName = "Add Expenditure", Module = expenditureManagement},
                new Permission
                    {Name = "expenditure_view", DisplayName = "View Expenditure", Module = expenditureManagement},
                new Permission
                    {Name = "expenditure_edit", DisplayName = "Edit Expenditure", Module = expenditureManagement},
                new Permission
                    {Name = "expenditure_delete", DisplayName = "Delete Expenditure", Module = expenditureManagement},
                new Permission {Name = "product_add", DisplayName = "Add Products", Module = productManagement},
                new Permission {Name = "product_view", DisplayName = "View Products", Module = productManagement},
                new Permission {Name = "product_edit", DisplayName = "Edit Products", Module = productManagement},
                new Permission {Name = "product_delete", DisplayName = "Delete Products", Module = productManagement},
                new Permission {Name = "stock_add", DisplayName = "Add Stock Keeping Item", Module = stockManagement},
                new Permission {Name = "stock_view", DisplayName = "View Stock Keeping Item", Module = stockManagement},
                new Permission {Name = "stock_edit", DisplayName = "Edit Stock Keeping Item", Module = stockManagement},
                new Permission
                    {Name = "stock_delete", DisplayName = "Delete Stock Keeping Item", Module = stockManagement},
                new Permission {Name = "sale_sell", DisplayName = "Sell Product", Module = saleManagement},
                new Permission {Name = "sale_edit", DisplayName = "Edit Sale", Module = saleManagement},
                new Permission {Name = "sale_record", DisplayName = "Record Sale", Module = saleManagement},
                new Permission {Name = "sale_view", DisplayName = "View Sales", Module = saleManagement},
                new Permission
                    {Name = "sale_invoice_view", DisplayName = "View Sale Invoices", Module = saleManagement},
                new Permission
                    {Name = "sale_invoice_change_date", DisplayName = "Change Invoice Date", Module = saleManagement},
                new Permission
                {
                    Name = "business_info_view", DisplayName = "View Business Information",
                    Module = businessInfoManagement
                },
                new Permission
                {
                    Name = "business_info_update", DisplayName = "Update Business Information",
                    Module = businessInfoManagement
                },
                new Permission {Name = "summary_view", DisplayName = "View Summaries", Module = dashboard},
                new Permission {Name = "stock_and_expiration_summary_view", DisplayName = "View Stock And Expiration Summaries", Module = dashboard},
                new Permission {Name = "sales_graph", DisplayName = "View Sales Graphs", Module = dashboard},
                new Permission
                {
                    Name = "most_selling_and_profitable_products_view",
                    DisplayName = "View Most Selling & Most Profitable Products", Module = dashboard
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
            if (newCount > 0)
            {
                logger.Information($"********************************************************");
                logger.Information($"*             {newCount} permissions added                      *");
                logger.Information($"********************************************************");
            }
        }
    }
}
