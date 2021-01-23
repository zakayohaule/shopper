using Shopper.Mvc.Enums;

namespace Shopper.Mvc.ViewModels
{
    public class DashboardSummary
    {
        public SummaryType SummaryType { get; set; }
        public int ItemsSold { get; set; }
        public long Sales { get; set; }
        public long Profit { get; set; }
        public long Expenditure { get; set; }
    }
}
