using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("sales")]
    public class Sale : BaseEntity<ulong>
    {
        [Column("sku_id")]
        public ulong SkuId { get; set; }

        [Column("product_id")]
        public uint ProductId { get; set; }

        [Column("sale_invoice_id")]
        public ulong SaleInvoiceId { get; set; }

        [Column("price")]
        public uint Price { get; set; }

        [Column("discount")] public uint Discount { get; set; } = 0;

        [Column("profit")] public long Profit { get; set; } = 0;

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("is_confirmed")] public bool IsConfirmed { get; set; } = false;

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [ForeignKey(nameof(SaleInvoiceId))]
        public SaleInvoice SaleInvoice { get; set; }
    }
}
