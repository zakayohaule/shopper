using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("product_images")]
    public class ProductImage : BaseEntity<ulong>
    {
        [Column("product_id"), Required]
        public uint ProductId { get; set; }

        [Column("image_path"), Required]
        public string ImagePath { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
