using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("product_groups")]
    public class ProductGroup : NoTenantBaseEntity<ushort>
    {
        [Column(name: "name"), Required]
        [Remote("ValidateProductGroupName", AdditionalFields = "Id"), DisplayName("Product Group Name")]
        public string Name { get; set; }

        [InverseProperty("ProductGroup")]
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
