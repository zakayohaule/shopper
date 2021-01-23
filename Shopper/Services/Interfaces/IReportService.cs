using System.Collections.Generic;
using System.Threading.Tasks;
using Shopper.Mvc.Entities;
using Shopper.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IReportService
    {
        public Task<List<Sale>> GetThisYearSalesAsync();
        public Task<StockExpiryModel> GetThisYearStockExpiryCountAsync();
        public Task<List<Expenditure>> GetThisYearExpenditure();
        public List<DashboardSummary> GetSummariesAsync(List<Sale> sales, List<Expenditure> expenditures);
    }
}
