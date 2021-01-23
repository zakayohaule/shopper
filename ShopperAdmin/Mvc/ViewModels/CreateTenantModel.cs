using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopperAdmin.Mvc.ViewModels
{
    public class CreateTenantModel
    {
        [Required, DisplayName("Name")]
        [Remote("ValidateTenantName", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required, DisplayName("Connection String")]
        public string ConnectionString { get; set; }

        [Required, DisplayName("Subdomain")]
        [Remote("ValidateTenantDomain", AdditionalFields = "Id")]
        public string Domain { get; set; }

        [Required, DisplayName("Tenant Code")]
        [Remote("ValidateTenantCode", AdditionalFields = "Id")]
        public string Code { get; set; }

        [Required, DisplayName("Name")]
        public string AdminFullName { get; set; }

        [Required, DisplayName("Email")]
        [Remote("ValidateTenantAdminEmail", AdditionalFields = "Id")]
        public string AdminEmail { get; set; }

        [Required, DisplayName("Default Role Name")]
        public string RoleName { get; set; }
    }
}
