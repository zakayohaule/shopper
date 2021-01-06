using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Mvc.ViewModels
{
    public class InvoiceDateModel
    {
        [Required, DisplayName("Invoice Date")]
        public DateTime InvoiceDate { get; set; }
    }
}
