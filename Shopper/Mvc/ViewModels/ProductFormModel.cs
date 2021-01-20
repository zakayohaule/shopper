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

        [DisplayName("Product Group"), Required]
        public ushort ProductGroupId { get; set; }

        [DisplayName("Product Category"), Required]
        public ushort ProductCategoryId { get; set; }

        [DisplayName("Product Type"), Required]
        public ushort ProductTypeId { get; set; }

        [DisplayName("Has Expiration Date")]
        public bool HasExpirationDate { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public IEnumerable<ushort> Attributes { get; set; }
    }
}
