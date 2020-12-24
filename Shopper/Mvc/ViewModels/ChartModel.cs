using System.Collections.Generic;
using Shared.Mvc.Entities;
using Shared.Mvc.Enums;

namespace Shopper.Mvc.ViewModels
{
    public class ChartModel
    {
        public string ChartId { get; set; }
        public SummaryType SummaryType { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
