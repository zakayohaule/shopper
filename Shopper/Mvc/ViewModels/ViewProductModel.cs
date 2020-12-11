using System.Collections.Generic;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class ViewProductModel
    {
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
    }
}
