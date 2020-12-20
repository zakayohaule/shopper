using System.Collections.Generic;
using Shared.Mvc.Entities;

namespace Shopper.Mvc.ViewModels
{
    public class DashboardModel
    {
        public List<DashboardSummary> Summaries { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Expenditure> Expenditures { get; set; }
    }
}
