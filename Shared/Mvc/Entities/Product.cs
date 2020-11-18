using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("products")]
    public class Product: BaseEntity<uint>
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("slug")]
        public string Slug { get; set; }

        [Column("product_category_id")]
        public ushort ProductCategoryId { get; set; }

        [ForeignKey(nameof(ProductCategoryId))]
        public ProductCategory ProductCategory { get; set; }

        [InverseProperty("Product")]
        public ICollection<ProductAttributeOption> AttributeOptions { get; set; }
    }
}
