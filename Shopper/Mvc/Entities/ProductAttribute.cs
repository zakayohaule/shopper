using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shopper.Mvc.Entities
{
    [Table("product_attribute")]
    public class ProductAttribute
    {
        [Column("attribute_id"), Required]
        public ushort AttributeId { get; set; }

        [Column("product_id"), Required]
        public uint ProductId { get; set; }

        [ForeignKey(nameof(AttributeId))]
        public Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

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
