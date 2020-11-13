using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.Mvc.ViewModels
{
    public class LoginModel
    {
        [EmailAddress, Required(ErrorMessage = "You must enter your email")]
        // [DisplayName("Email Address")]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "Must be more than 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}