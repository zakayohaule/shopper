using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Other;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Mvc.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private IUserClaimService _userClaimService;
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

            if (_userClaimService.HasAnyPermission(userId, "tenant_view,database_view"))
            {
                var tenantManagement = ModuleHelper.AddTree("Tenant Management", "fa fa-house");
                links = new List<SidebarMenu>();

                if (_userClaimService.HasPermission(userId, "database_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: "Databases", url: Url.Action("Index", "User")));
                }

                if (_userClaimService.HasPermission(userId, "tenant_view"))
                {
                    links.Add(ModuleHelper.AddModuleLink(name: "Tenants", Url.Action("Index", "Tenant")));
                }

                tenantManagement.TreeChild = links;
                sidebars.Add(tenantManagement);
            }
            /*var userManagementModule = ModuleHelper.AddTree(name: "User Management", iconClassName: "fa fa-user");
            userManagementModule.TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModuleLink(name: "Users", url: "/", iconClassName:"fa fa-users"),
                ModuleHelper.AddModuleLink(name: "Roles", url: "/Roles", null),
                ModuleHelper.AddModuleLink(name: "Permissions", url: "/permissions", null)
            };
            sidebars.Add(userManagementModule);*/

            return View(sidebars);
        }
    }
}
