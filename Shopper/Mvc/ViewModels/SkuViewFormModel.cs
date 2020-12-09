using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class SkuViewFormModel
    {
        public Product Product { get; set; }
        public Sku Sku { get; set; }

        [Required] public uint ProductId { get; set; }
        [Required] public string Quantity { get; set; }
        [Required] public string BuyingPrice { get; set; }
        [Required] public string SellingPrice { get; set; }
        [Required] public string MaximumDiscount { get; set; }

        [Required] public IFormFile Image { get; set; }

        [Required] public List<string> Attributes { get; set; }
    }
}
