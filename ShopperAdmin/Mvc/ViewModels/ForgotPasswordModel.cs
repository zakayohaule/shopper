using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopperAdmin.Mvc.ViewModels
{
    public class ForgotPasswordModel
    {
        [DisplayName("Email"), Required]
        public string Email { get; set; }

        public string ResetLink { get; set; }

        public string FullName { get; set; }
    }
}
