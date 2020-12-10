using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopper.Mvc.ViewModels
{
    public class SaleFormViewModel
    {
        public ulong Id { get; set; }

        public List<SelectListItem> Skus { get; set; }

        public List<SelectListItem> SkuPrices { get; set; }

        [Required] public ulong SkuId { get; set; }

        [Remote("CheckStockQuantity", AdditionalFields = nameof(SkuId))]
        [Required]
        public int Quantity { get; set; } = 1;

        [Required] public string Price { get; set; }
        public string SellingPrice { get; set; }
    }
}
