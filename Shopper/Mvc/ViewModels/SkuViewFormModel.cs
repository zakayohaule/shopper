using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class SkuViewFormModel
    {
        public Product Product { get; set; }

        public Sku Sku { get; set; }

        [Display(Name = "Stock Date")]
        [Required]
        public DateTime StockDate { get; set; }

        [Required] public uint ProductId { get; set; }

        [Required] public string Quantity { get; set; }

        [Display(Name = "Buying Price")]
        [Required]
        public string BuyingPrice { get; set; }

        [Display(Name = "Selling Price")]
        [Required]
        [Remote(routeName: "ValidateSellingPrice", AdditionalFields = nameof(BuyingPrice))]
        public string SellingPrice { get; set; }

        [Display(Name = "Maximum Discount")]
        [Required]
        [Remote(routeName: "ValidateMaximumDiscount", AdditionalFields = nameof(BuyingPrice)+","+nameof(SellingPrice))]
        public string MaximumDiscount { get; set; }

        [DisplayName("Receive low stock alert?")]
        public bool ReceiveLowStockAlert { get; set; } = true;

        [DisplayName("Low stock quantity")] public int? LowStockQuantity { get; set; } = 1;

        [Required] public IFormFile Image { get; set; }

        [Required] public List<string> Attributes { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public List<AttributeSelect> AttributeSelects { get; set; }
    }
}
