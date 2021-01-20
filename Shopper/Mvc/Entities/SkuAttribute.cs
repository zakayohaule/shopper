using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("sku_attributes")]
    public class SkuAttribute : BaseEntity<ulong>
    {
        [Column("attribute_option_id"), Required]
        public ushort AttributeOptionId { get; set; }

        [Column("stock_keeping_unit_id"), Required]
        public ulong SkuId { get; set; }

        [ForeignKey(nameof(AttributeOptionId))]
        public AttributeOption Option { get; set; }

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }

    }
}
