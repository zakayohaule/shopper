using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class SaleViewModal
    {
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public ulong InvoiceId { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public ulong TotalAmount { get; set; } = 0;
        public uint TotalDiscount { get; set; } = 0;
    }
}
