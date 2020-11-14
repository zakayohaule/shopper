using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using Shared.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private IUserClaimService _userClaimService;
        
        public SidebarViewComponent(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        public IViewComponentResult Invoke(string filter)
        {
            //you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();

            //if (((ClaimsPrincipal)User).GetUserProperty("AccessProfile").Contains("VES_008, Payroll"))
            //{\
            //}

            sidebars.Add(ModuleHelper.AddHeader("SETTINGS"));
            var userManagementModule = ModuleHelper.AddTree(name: "User Management", iconClassName: "fa fa-user");
            userManagementModule.TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModuleLink(name: "Users", url: Url.Action("Index", "User"), iconClassName:"fa fa-users"),
                ModuleHelper.AddModuleLink(name: "Roles", url: "/Roles", null),
                ModuleHelper.AddModuleLink(name: "Permissions", url: "/permissions", null)
            };
            sidebars.Add(userManagementModule);

            return View(sidebars);
        }
    }
}
