using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopper.Mvc.ViewModels
{
    public class SaleFormViewModel
    {
        public List<SelectListItem> Skus { get; set; }

        [Required] public ulong SkuId { get; set; }
        [Required] public int Quantity { get; set; } = 1;
        [Required] public string Price { get; set; }
        public string SellingPrice { get; set; }
    }
}
