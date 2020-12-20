using System.Collections.Generic;
using Shared.Mvc.Enums;

namespace Shopper.Mvc.ViewModels
{
    public class DashboardSummary
    {
        public SummaryType SummaryType { get; set; }
        public uint ItemsSold { get; set; }
        public ulong Sales { get; set; }
        public ulong Profit { get; set; }
        public ulong Expenditure { get; set; }
    }
}
