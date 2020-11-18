using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("product_attribute_options")]
    public class ProductAttributeOption
    {
        [Column("attribute_option_id")] public ushort AttributeOptionId { get; set; }

        [Column("product_id")] public uint ProductId { get; set; }

        [ForeignKey(nameof(AttributeOptionId))] public AttributeOption AttributeOption { get; set; }

        [ForeignKey(nameof(ProductId))] public Product Product { get; set; }

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
