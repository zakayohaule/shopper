using System.ComponentModel.DataAnnotations.Schema;
using ShopperAdmin.Mvc.Entities.BaseEntities;

namespace ShopperAdmin.Mvc.Entities.Identity
{
    [Table(("permissions"))]
    public class Permission : BaseEntity<ushort>
    {
        [Column("name")] public string Name { get; set; }

        [Column("display_name")] public string DisplayName { get; set; }

        [Column("module_id")] public ushort ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; }
    }
}
