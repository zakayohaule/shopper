using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities.Identity
{
    [Table("modules")]
    public class Module : NoTenantBaseEntity<ushort>
    {
        [Column("name")] public string Name { get; set; }

        [InverseProperty("Module")] public List<Permission> Permissions { get; set; }
    }
}
