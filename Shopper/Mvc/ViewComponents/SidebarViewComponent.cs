using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shopper.Mvc.Entities;
using Shopper.Other;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IUserClaimService _userClaimService;
        private readonly ITranslator _translator;

        private long UserId { get; set; }

        public SidebarViewComponent(IUserClaimService userClaimService, ITranslator translator)
        {
            _userClaimService = userClaimService;
            _translator = translator;
        }

        public IViewComponentResult Invoke(long userId)
        {
            UserId = userId;
            var sidebars = new List<SidebarMenu>();
            var links = new List<SidebarMenu>();

            // sidebars.Add(ModuleHelper.AddHeader("  User Management"));
            sidebars.Add(ModuleHelper.AddModuleLink(_translator["Dashboard"],
                Url.Action("Index", "Home"), "fa fa-home"));
            if (_userClaimService.HasPermission(userId, "sale_record"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink(_translator["Add Sale"],
                    Url.Action("Create", "Sale"), "fas fa-shopping-cart"));
            }

            if (_userClaimService.HasPermission(userId, "sale_my_sells"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink(_translator["My Sales"],
                    Url.Action("Create", "Sale"), "fas fa-shopping-cart"));
            }
            if (_userClaimService.HasPermission(userId, "expenditure_add"))
            {
                sidebars.Add(ModuleHelper.AddModuleLink(_translator["Expenditures"],
                    Url.Action("Index", "Expenditure"), "fas fa-money-bill-wave"));
            }

            //User Management Module
            if (_userClaimService.HasAnyPermission(userId, "user_view,role_view"))
            {
                var userManagement = ModuleHelper.AddTree(_translator["User Management"], "fa fa-users");
                links = new List<SidebarMenu>();

                if (_userClaimService.HasPermission(userId, "user_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: _translator["Users"],
                        url: Url.Action("Index", "User"), "fa fa-users"));
                }

                if (_userClaimService.HasPermission(userId, "role_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: _translator["Roles"],
                        Url.Action("Index", "Role"), "fas fa-user-tag"));
                }

                userManagement.TreeChild = links;
                sidebars.Add(userManagement);
            }


            //Settings Settings Module
            if (_userClaimService.HasAnyPermission(userId, "business_info_view, price_type_view," +
                                                           " expenditure_type_view"))
            {
                var settings = ModuleHelper.AddTree(_translator["Settings"], "fas fa-cog");
                links = new List<SidebarMenu>();
                if (_userClaimService.HasPermission(userId, "business_info_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:_translator["Business Info"],
                        Url.Action("Show", "Business"), "fas fa-sitemap"));
                }
                if (_userClaimService.HasPermission(userId, "price_type_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Price Types"],
                        Url.Action("Index", "PriceType")));
                }
                if (_userClaimService.HasPermission(userId, "expenditure_type_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Expenditure Types"],
                        Url.Action("Index", "ExpenditureType")));
                }


                settings.TreeChild = links;
                sidebars.Add(settings);
            }

            // Product Management
            if (_userClaimService.HasAnyPermission(userId, "product_group_view, product_category_view, product_type_view," +
                                                           " product_view, stock_view, sale_view"))
            {
                var productManagement = ModuleHelper.AddTree(_translator["Product Management"]);
                links = new List<SidebarMenu>();
                if (_userClaimService.HasPermission(userId, "product_group_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:_translator["Product Groups"], Url.Action("Index", "ProductGroup"), "fas fa-sitemap"));
                }
                if (_userClaimService.HasPermission(userId, "product_category_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:_translator["Product Categories"], Url.Action("Index", "ProductCategory")));
                }
                if (_userClaimService.HasPermission(userId, "product_type_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name:_translator["Product Types"], Url.Action("Index", "ProductType")));
                }
                if (_userClaimService.HasPermission(userId, "attribute_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Product Attributes"], Url.Action("Index", "Attribute"), "fas fa-paint-brush"));
                }
                if (_userClaimService.HasPermission(userId, "product_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Products"], Url.Action("Index", "Product"), "fas fa-tshirt"));
                }
                if (_userClaimService.HasPermission(userId, "stock_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Stock"], Url.Action("Index", "Stock")));
                }
                if (_userClaimService.HasPermission(userId, "sale_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(_translator["Sale Invoices"], Url.Action("Index", "Sale")));
                }

                productManagement.TreeChild = links;
                sidebars.Add(productManagement);
            }
            return View(sidebars);
        }

    }
}
