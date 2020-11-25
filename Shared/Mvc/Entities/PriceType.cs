using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("price_types")]
    public class PriceType : BaseEntity<ushort>
    {

        [Column(name: "name"), Required]
        [Remote("ValidatePriceTypeName", AdditionalFields = "Id"), DisplayName("Price type")]
        public string Name { get; set; }
    }
}
