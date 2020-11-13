using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Mvc.ViewModels
{
    public class ResetPasswordModel
    {
        [HiddenInput]
        public string Email { get; set; }
        
        [HiddenInput]
        public string Token { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string NewPassword { get; set; }
        
        [Required, Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password must match")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}