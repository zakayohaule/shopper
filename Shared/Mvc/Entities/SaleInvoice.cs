using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;

namespace Shared.Mvc.Entities
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

        [InverseProperty("SaleInvoice")]
        public ICollection<Sale> Sales { get; set; }
    }
}
