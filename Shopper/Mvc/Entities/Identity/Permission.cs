using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities.Identity
{
    [Table(("permissions"))]
    public class Permission : NoTenantBaseEntity<ushort>
    {
        [Column("name")] public string Name { get; set; }

        [Column("display_name")] public string DisplayName { get; set; }

        [Column("module_id")] public ushort ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; }
    }
}
