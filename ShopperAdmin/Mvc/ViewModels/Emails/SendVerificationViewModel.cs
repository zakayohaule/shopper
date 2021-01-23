using System.ComponentModel.DataAnnotations;

namespace ShopperAdmin.Mvc.ViewModels.Emails
{
    public class SendVerificationViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
