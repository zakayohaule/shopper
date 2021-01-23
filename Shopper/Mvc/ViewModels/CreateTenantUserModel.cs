using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Shopper.Mvc.ViewModels
{
    public class CreateTenantUserModel
    {
        [Required, DisplayName("Name")]
        public string AdminFullName { get; set; }

        [Required, DisplayName("Email")]
        [Remote("ValidateTenantAdminEmail", AdditionalFields = "Id")]
        public string AdminEmail { get; set; }

        [Required, DisplayName("Default Role Name")]
        public string RoleName { get; set; }
    }
}
