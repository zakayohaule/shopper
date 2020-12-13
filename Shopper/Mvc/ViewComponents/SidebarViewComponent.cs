using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using Shared.Mvc.Entities;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IUserClaimService _userClaimService;
        private long UserId { get; set; }

        public SidebarViewComponent(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        public IViewComponentResult Invoke(long userId)
        {
            UserId = userId;
            var sidebars = new List<SidebarMenu>();
            var links = new List<SidebarMenu>();

            // sidebars.Add(ModuleHelper.AddHeader("  User Management"));

            if (_userClaimService.HasPermission(userId, "sale_record"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Add Sale", Url.Action("Create", "Sale"), "fas fa-shopping-cart"));
            }

            //User Management Module
            if (_userClaimService.HasAnyPermission(userId, "user_view,role_view"))
            {
                var userManagement = ModuleHelper.AddTree("User Management", "fa fa-users");
                links = new List<SidebarMenu>();

                if (_userClaimService.HasPermission(userId, "user_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: "Users", url: Url.Action("Index", "User"), "fa fa-users"));
                }

                if (_userClaimService.HasPermission(userId, "role_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: "Roles", Url.Action("Index", "Role"), "fas fa-user-tag"));
                }

                userManagement.TreeChild = links;
                sidebars.Add(userManagement);
            }


            //Settings Settings Module
            if (_userClaimService.HasAnyPermission(userId, "product_group_view,product_category_view,attribute_view,price_type_view"))
            {
                var settings = ModuleHelper.AddTree("Settings", "fas fa-cog");
                links = new List<SidebarMenu>();
                if (_userClaimService.HasPermission(userId, "product_group_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:"Product Groups", Url.Action("Index", "ProductGroup"), "fas fa-sitemap"));
                }
                if (_userClaimService.HasPermission(userId, "product_category_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:"Product Categories", Url.Action("Index", "ProductCategory")));
                }
                if (_userClaimService.HasPermission(userId, "attribute_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Product Attributes", Url.Action("Index", "Attribute"), "fas fa-paint-brush"));
                }
                if (_userClaimService.HasPermission(userId, "price_type_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Price Types", Url.Action("Index", "PriceType")));
                }
                if (_userClaimService.HasPermission(userId, "expenditure_type_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Expenditure Types", Url.Action("Index", "ExpenditureType")));
                }

                settings.TreeChild = links;
                sidebars.Add(settings);
            }

            // Product Management
            if (_userClaimService.HasAnyPermission(userId, "product_view, stock_view, sale_view"))
            {
                var productManagement = ModuleHelper.AddTree("Product Management");
                links = new List<SidebarMenu>();
                if (_userClaimService.HasPermission(userId, "product_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Products", Url.Action("Index", "Product"), "fas fa-tshirt"));
                }
                if (_userClaimService.HasPermission(userId, "stock_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Stock", Url.Action("Index", "Stock")));
                }
                if (_userClaimService.HasPermission(userId, "sale_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink("Sale Invoices", Url.Action("Index", "Sale")));
                }

                productManagement.TreeChild = links;
                sidebars.Add(productManagement);
            }
            return View(sidebars);
        }

    }
}
