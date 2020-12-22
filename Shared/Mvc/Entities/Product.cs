using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("products")]
    public class Product: BaseEntity<uint>
    {
        [Column("name"), Required]
        [Remote("ValidateProductName", AdditionalFields = "Id"), DisplayName("Product Name")]
        public string Name { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("slug")]
        public string Slug { get; set; }

        [Column("product_category_id"), Required, Display(Name = "Product category")]
        public ushort ProductCategoryId { get; set; }

        [ForeignKey(nameof(ProductCategoryId))]
        public ProductCategory ProductCategory { get; set; }

        [Column("has_expiration")]
        public bool HasExpiration { get; set; } = false;

        [InverseProperty("Product")]
        public List<Sku> Skus { get; set; }

        [InverseProperty("Product")]
        public ICollection<ProductAttribute> Attributes { get; set; }

        [InverseProperty("Product")]
        public ICollection<ProductImage> Images { get; set; }
    }
}
