using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;

namespace Shopper.Mvc.Entities
{
    [Table("price_types")]
    public class PriceType : BaseEntity<ushort>
    {

        [Column(name: "name"), Required]
        // [Remote("ValidatePriceTypeName", AdditionalFields = "Id"), DisplayName("Price type")]
        public string Name { get; set; }
    }
}
