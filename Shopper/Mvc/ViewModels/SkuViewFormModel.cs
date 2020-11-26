using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class SkuViewFormModel
    {
        public Product Product { get; set; }
        public Sku Sku { get; set; }

        [Required]
        public List<string> Attributes { get; set; }
    }
}
