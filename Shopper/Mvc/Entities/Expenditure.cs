using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Mvc.Entities
{
    [Table("expenditures")]
    public class Expenditure : BaseEntity<ulong>
    {
        [Column("user_id")]
        public long? UserId { get; set; }

        [Column("amount")]
        public uint Amount { get; set; }

        [Column("expenditure_date")]
        public DateTime Date { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("expenditure_type_id")]
        public ushort ExpenditureTypeId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(ExpenditureTypeId))]
        public ExpenditureType ExpenditureType { get; set; }
    }
}
