using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities.Identity
{
    [Table("modules")]
    public class Module : NoTenantBaseEntity<ushort>
    {
        [Column("name")] public string Name { get; set; }

        [InverseProperty("Module")] public List<Permission> Permissions { get; set; }
    }
}
