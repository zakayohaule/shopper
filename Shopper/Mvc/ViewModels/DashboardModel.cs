using System.Collections.Generic;
using Shared.Mvc.Entities;
using Shared.Mvc.Enums;

namespace Shopper.Mvc.ViewModels
{
    public class DashboardModel
    {
        public List<DashboardSummary> Summaries { get; set; }
        public SummaryType SummaryType { get; set; }
        public List<Sale> Sales { get; set; }
        public StockExpiryModel StockExpiryModel { get; set; }
        public List<Expenditure> Expenditures { get; set; }
    }
}
