using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("sales")]
    public class Sale : BaseEntity<ulong>
    {
        [Column("sku_id")]
        public ulong SkuId { get; set; }

        [Column("sale_invoice_id")]
        public ulong SaleInvoiceId { get; set; }

        [Column("price")]
        public uint Price { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("is_confirmed")] public bool IsConfirmed { get; set; } = true;

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }

        [ForeignKey(nameof(SaleInvoiceId))]
        public SaleInvoice SaleInvoice { get; set; }
    }
}
