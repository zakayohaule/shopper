using System;
using System.ComponentModel.DataAnnotations;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class ExpenditureModel
    {
        [Required]
        public string Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Display(Name="Expenditure Type")]
        public ushort ExpenditureTypeId { get; set; }
    }
}
