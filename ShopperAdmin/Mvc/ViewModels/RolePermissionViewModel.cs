using System.Collections.Generic;
using ShopperAdmin.Mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Mvc.ViewModels
{
    [Bind("RoleClaims")]
    public class RolePermissionViewModel
    {
        public string  RoleName { get; set; }
        public long RoleId { get; set; }
        public List<Module> Modules { get; set; }
        public List<string> RoleClaims { get; set; }
    }
}
