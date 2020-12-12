using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopper.Mvc.ViewModels
{
    public class UpdateImageModel
    {
        [Required]
        [Display(Name="Image")]
        public IFormFile Image { get; set; }
    }
}
