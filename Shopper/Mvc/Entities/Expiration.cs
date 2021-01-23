using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("expirations")]
    public class Expiration : BaseEntity<ulong>
    {
        [Column("sku_Id")]
        public ulong SkuId { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey(nameof(SkuId))]
        public Sku Sku { get; set; }

    }
}
