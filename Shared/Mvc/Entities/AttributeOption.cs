using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("attribute_options")]
    public class AttributeOption : BaseEntity<ushort>
    {
        [Column(name: "name")]
        public string Name { get; set; }

        [Column("attribute_id")]
        public ushort AttributeId { get; set; }

        [ForeignKey(nameof(AttributeId))]
        public Attribute Attribute { get; set; }
    }
}
