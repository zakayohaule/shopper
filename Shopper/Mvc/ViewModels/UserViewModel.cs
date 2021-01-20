using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Mvc.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required, DisplayName("Full Name"), BindRequired]
        public string FullName { get; set; }

        [Required, EmailAddress, DisplayName("Email Address"), BindRequired]
        [Remote("ValidateUserEmail", AdditionalFields = "Id")]
        public string Email { get; set; }

        public List<Role> Roles { get; set; }
    }
}
