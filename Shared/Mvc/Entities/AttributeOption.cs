using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("attribute_options")]
    public class AttributeOption : BaseEntity<ushort>
    {
        [Column(name: "name")]
        [Required]
        [Remote("ValidateAttributeOptionName", AdditionalFields = nameof(Id))]
        public string Name { get; set; }

        [Column("attribute_id")]
        [Required]
        public ushort AttributeId { get; set; }

        [ForeignKey(nameof(AttributeId))]
        public Attribute Attribute { get; set; }
    }
}
