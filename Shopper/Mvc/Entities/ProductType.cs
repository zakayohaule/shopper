using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("product_types")]
    public class ProductType : NoTenantBaseEntity<ushort>
    {
        [Column(name: "name"), Required]
        public string Name { get; set; }

        [Column("product_category_id"), Required]
        public ushort ProductCategoryId { get; set; }

        [ForeignKey(nameof(ProductCategoryId))]
        public ProductCategory ProductCategory { get; set; }

        [InverseProperty("ProductType")]
        public List<Product> Products { get; set; }
    }
}
