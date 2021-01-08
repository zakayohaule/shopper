using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("expenditure_types")]
    public class ExpenditureType : BaseEntity<ushort>
    {
        [Column(name: "name"), Required]
        // [Remote("ValidateExpenditureTypeName", AdditionalFields = "Id"), DisplayName("Expenditure Type")]
        public string Name { get; set; }

        [InverseProperty("ExpenditureType")]
        public ICollection<Expenditure> ExpenditureTypes { get; set; }
    }
}
