using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    public class Sku : BaseEntity<ulong>
    {
        [Column("product_id")]
        public uint ProductId { get; set; }

        [Column("buying_price")]
        public decimal BuyingPrice { get; set; }

        [Column("selling_price")]
        public decimal SellingPrice { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("remaining_quantity")]
        public int RemainingQuantity { get; set; }

        [Column("maximum_discount")]
        public decimal MaximumDiscount { get; set; }

        [Column("is_on_sale")]
        public bool IsOnSale { get; set; }

        [InverseProperty("Sku")]
        public ICollection<SkuAttribute> SkuAttributes { get; set; }
    }
}
