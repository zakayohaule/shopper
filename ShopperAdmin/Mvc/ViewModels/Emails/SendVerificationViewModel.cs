using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopperAdmin.Mvc.ViewModels.Emails
{
    public class SendVerificationViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
