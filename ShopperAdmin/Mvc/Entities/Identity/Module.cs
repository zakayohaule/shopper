using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ShopperAdmin.Mvc.Entities.BaseEntities;
using ShopperAdmin.Mvc.Entities.BaseEntities;

namespace ShopperAdmin.Mvc.Entities.Identity
{
    [Table("modules")]
    public class Module : BaseEntity<ushort>
    {
        [Column("name")] public string Name { get; set; }

        [InverseProperty("Module")] public List<Permission> Permissions { get; set; }
    }
}
