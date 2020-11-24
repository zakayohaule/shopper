using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("price_types")]
    public class PriceType : BaseEntity<ushort>
    {
        [Column(name: "name")] public string Name { get; set; }
    }
}
