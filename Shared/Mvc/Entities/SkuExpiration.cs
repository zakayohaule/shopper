using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("sku_expirations")]
    public class SkuExpiration : BaseEntity<ulong>
    {
        [Column("sku_id")]
        public ulong SkuId { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }
    }
}
