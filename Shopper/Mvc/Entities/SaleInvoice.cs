using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shopper.Mvc.Entities.BaseEntities;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Mvc.Entities
{
    [Table("sale_invoices")]
    public class SaleInvoice : BaseEntity<ulong>
    {
        [Column("number")]
        public string Number { get; set; }

        [Column("amount")]
        public ulong Amount { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; } = false;

        [Column("is_canceled")]
        public bool IsCanceled { get; set; } = false;

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [InverseProperty("SaleInvoice")]
        public ICollection<Sale> Sales { get; set; }
    }
}
