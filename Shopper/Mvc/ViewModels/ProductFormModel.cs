using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopper.Mvc.ViewModels
{
    public class ProductFormModel
    {
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("product category")]
        public ushort ProductCategoryId { get; set; }

        [DisplayName("Has Expiration Date")]
        public bool HasExpirationDate { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public IEnumerable<ushort> Attributes { get; set; }
    }
}
