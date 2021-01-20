using System.Collections.Generic;
using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.Identity;

namespace Shared.Mvc.ViewModels
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