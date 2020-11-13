using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Mvc.Entities.Identity
{
    [Table(("permissions"))]
    public class Permission
    {
        [Column("id")] public ushort Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("display_name")] public string DisplayName { get; set; }

        [Column("module_id")] public short ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))] public Module Module { get; set; }
    }
}