using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("sku_selling_prices")]
    public class SkuSellingPrice
    {
        [Column("sku_id"), Required]
        public ulong SkuId { get; set; }

        [Column("price_type_id"), Required]
        public ushort PriceTypeId { get; set; }

        [Column("price")]
        public uint? Price { get; set; }

        [ForeignKey(nameof(SkuId))] public Sku Sku { get; set; }

        [ForeignKey(nameof(PriceTypeId))] public PriceType PriceType { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BindNever]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [BindNever]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
