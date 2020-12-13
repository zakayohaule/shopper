using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
{
    [Table("expenditures")]
    public class Expenditure : BaseEntity<ulong>
    {
        [Column("amount")]
        public uint Amount { get; set; }

        [Column("expenditure_date")]
        public DateTime Date { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("expenditure_type_id")]
        public ushort ExpenditureTypeId { get; set; }

        [ForeignKey(nameof(ExpenditureTypeId))]
        public ExpenditureType ExpenditureType { get; set; }
    }
}
