using System.ComponentModel.DataAnnotations;

namespace Shopper.Mvc.ViewModels.Emails
{
    public class SendVerificationViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
