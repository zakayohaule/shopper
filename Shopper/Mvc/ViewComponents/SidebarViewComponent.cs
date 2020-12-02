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
            sidebars.Add(ModuleHelper.AddHeader("  User Management"));

            if (_userClaimService.HasPermission(UserId, "user_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink(name: "Users", url: Url.Action("Index", "User"), "fa fa-users"));
            }
            if (_userClaimService.HasPermission(UserId, "role_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink(name: "Roles", Url.Action("Index", "Role"), "fas fa-user-tag"));
            }

            sidebars.Add(ModuleHelper.AddHeader("Product Management"));
            if (_userClaimService.HasPermission(UserId, "product_group_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Product Groups", Url.Action("Index", "ProductGroup"), "fas fa-sitemap"));
            }
            if (_userClaimService.HasPermission(UserId, "product_category_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Product Categories", Url.Action("Index", "ProductCategory")));
            }
            if (_userClaimService.HasPermission(UserId, "product_attribute_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Product Attributes", Url.Action("Index", "Attribute"), "fas fa-paint-brush"));
            }
            if (_userClaimService.HasPermission(UserId, "price_type_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Price Types", Url.Action("Index", "PriceType")));
            }
            if (_userClaimService.HasPermission(UserId, "product_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Products", Url.Action("Index", "Product"), "fas fa-tshirt"));
            }
            if (_userClaimService.HasPermission(UserId, "stock_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Stock", Url.Action("Index", "Stock")));
            }
            if (_userClaimService.HasPermission(UserId, "sale_view"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink("Sales", Url.Action("Index", "Sale")));
            }
            return View(sidebars);
        }
    }
}
