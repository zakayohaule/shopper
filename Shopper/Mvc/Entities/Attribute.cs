using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("attributes")]
    public class Attribute: BaseEntity<ushort>
    {
        [Column(name: "name")]
        [Required]
        // [Remote("ValidateAttributeName", AdditionalFields = nameof(Id))]
        public string Name { get; set; }

        [InverseProperty("Attribute")]
        public ICollection<AttributeOption> AttributeOptions { get; set; }
    }
}
