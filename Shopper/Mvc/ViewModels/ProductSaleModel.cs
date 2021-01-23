using System.Linq;
using Shopper.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class ProductSaleModel
    {
        public Product Product { get; set; }
        public IGrouping<uint?, Sale> Sales { get; set; }
    }
}
