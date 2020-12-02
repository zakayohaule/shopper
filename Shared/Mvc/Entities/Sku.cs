using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("skus")]
    public class Sku : BaseEntity<ulong>
    {

        [Column("product_id"), Required]
        public uint ProductId { get; set; }

        [Column("buying_price"), Required]
        [Display(Name = "Buying price (unit)")]
        public uint BuyingPrice { get; set; }

        [Column("selling_price"), Required]
        [Display(Name = "Selling price (unit)")]
        public uint SellingPrice { get; set; }

        [Column("quantity"), Required]
        public int Quantity { get; set; }

        [Column("remaining_quantity")]
        [Display(Name = "Remaining Quantity")]
        public int RemainingQuantity { get; set; }

        [Column("maximum_discount")]
        [Display(Name = "Discount")]
        public uint MaximumDiscount { get; set; } = 0;

        [Column("is_on_sale")]
        public bool IsOnSale { get; set; } = false;

        [ForeignKey(nameof(ProductId))]
        public Product Product{ get; set; }

        [InverseProperty("Sku")]
        public ICollection<SkuAttribute> SkuAttributes { get; set; }

        [InverseProperty(("Sku"))]
        public ICollection<Sale> Sales { get; set; }
    }
}
