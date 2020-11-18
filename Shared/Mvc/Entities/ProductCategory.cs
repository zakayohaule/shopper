using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("product_categories")]
    public class ProductCategory : BaseEntity<ushort>
    {
        [Column(name: "name")] public string Name { get; set; }

        [Column("product_group_id")] public ushort ProductGroupId { get; set; }

        [ForeignKey(nameof(ProductGroupId))] public ProductGroup ProductGroup { get; set; }

        [InverseProperty("ProductCategory")]
        public ICollection<Product> Products { get; set; }
    }
}
