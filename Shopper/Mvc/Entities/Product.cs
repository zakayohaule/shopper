using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("products")]
    public class Product: BaseEntity<uint>
    {
        [Column("name"), Required]
        // [Remote("ValidateProductName", AdditionalFields = "Id"), DisplayName("Product Name")]
        public string Name { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("slug")]
        public string Slug { get; set; }

        [Column("product_type_id"), Required, Display(Name = "Product type")]
        public ushort ProductTypeId { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public ProductType ProductType { get; set; }

        [Column("has_expiration")]
        public bool HasExpiration { get; set; } = false;

        [InverseProperty("Product")]
        public List<Sku> Skus { get; set; }

        [InverseProperty("Product")]
        public List<Sale> Sales { get; set; }

        [InverseProperty("Product")]
        public ICollection<ProductAttribute> Attributes { get; set; }

        [InverseProperty("Product")]
        public ICollection<ProductImage> Images { get; set; }
    }
}
