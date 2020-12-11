using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopper.Mvc.ViewModels
{
    public class ProductFormModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public ushort ProductCategoryId { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        public IEnumerable<ushort> Attributes { get; set; }
    }
}
