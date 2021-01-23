using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopperAdmin.Mvc.ViewModels
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Required, Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password must match")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
