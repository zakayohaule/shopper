using System.Collections.Generic;

namespace Shopper.Mvc.ViewModels
{
    public class ProductFormModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public ushort ProductCategoryId { get; set; }
        public IEnumerable<ushort> Attributes { get; set; }
    }
}
