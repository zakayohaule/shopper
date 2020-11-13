using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Mvc.Entities.Identity
{
    [Table("modules")]
    public class Module
    {
        [Column("id")] public short Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [InverseProperty("Module")] public List<Permission> Permissions { get; set; }
    }
}