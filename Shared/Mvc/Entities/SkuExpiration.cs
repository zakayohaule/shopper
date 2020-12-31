using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Mvc.Entities
{
    [Table("sku_expirations")]
    public class SkuExpiration
    {
        [Column("sku_id")]
        public ulong SkuId { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }
    }
}
