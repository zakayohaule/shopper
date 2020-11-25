using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("attributes")]
    public class Attribute: BaseEntity<ushort>
    {
        [Column(name: "name")]
        [Required]
        public string Name { get; set; }

        [InverseProperty("Attribute")]
        public ICollection<AttributeOption> AttributeOptions { get; set; }
    }
}
