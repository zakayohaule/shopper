using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Shared.Mvc.Enums;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReportService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Sale>> GetThisYearSalesAsync()
        {
            return await _dbContext.Sales
                .Include(s => s.SaleInvoice)
                .Where(s => s.SaleInvoice.Date.Year == DateTime.Today.Year && s.IsConfirmed)
                .ToListAsync();
        }

        public async Task<List<Expenditure>> GetThisYearExpenditure()
        {
            return await _dbContext.Expenditures
                .Where(ex => ex.Date.Year == DateTime.Today.Year)
                .ToListAsync();
        }

        public List<DashboardSummary> GetSummariesAsync(List<Sale> annualSales,
            List<Expenditure> annualExpenditures)
        {
            var sales = annualSales;

            var expenditure = annualExpenditures;

            var summaries = new List<DashboardSummary>
            {
                GetTodaySummary(sales, expenditure),
                GetWeekSummary(sales, expenditure),
                GetMonthSummary(sales, expenditure),
                GetYearSummary(sales, expenditure)
            };
            return summaries;
        }

        public DashboardSummary GetTodaySummary(List<Sale> sales, List<Expenditure> expenditures)
        {
            var itemsSold = sales
                .Where(s => s.SaleInvoice.Date.Date.IsToday())
                .Sum(s => s.Quantity);
            var totalSales = sales
                .Where(s => s.SaleInvoice.Date.Date.IsToday())
                .Sum(s => (s.Quantity * s.Price));
            var profit = sales
                .Where(s => s.SaleInvoice.Date.Date.IsToday())
                .Sum(s => (s.Quantity * s.Profit));
            var expenditure = expenditures
                .Where(e => e.Date.Date.IsToday())
                .Sum(e => e.Amount);
            return new DashboardSummary
            {
                SummaryType = SummaryType.Today,
                ItemsSold = (uint) itemsSold,
                Sales = (ulong) totalSales,
                Expenditure = (ulong) expenditure,
                Profit = (ulong) (profit - expenditure)
            };
        }

        public DashboardSummary GetWeekSummary(List<Sale> sales, List<Expenditure> expenditures)
        {
            var itemsSold = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisWeek())
                .Sum(s => s.Quantity);
            var totalSales = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisWeek())
                .Sum(s => (s.Quantity * s.Price));
            var profit = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisWeek())
                .Sum(s => (s.Quantity * s.Profit));
            var expenditure = expenditures
                .Where(e => e.Date.Date.WithinThisWeek())
                .Sum(e => e.Amount);
            return new DashboardSummary
            {
                SummaryType = SummaryType.Week,
                ItemsSold = (uint) itemsSold,
                Sales = (ulong) totalSales,
                Expenditure = (ulong) expenditure,
                Profit = (ulong) (profit - expenditure)
            };
        }

        public DashboardSummary GetMonthSummary(List<Sale> sales, List<Expenditure> expenditures)
        {
           var itemsSold = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisMonth())
                .Sum(s => s.Quantity);
            var totalSales = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisMonth())
                .Sum(s => (s.Quantity * s.Price));
            var profit = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisMonth())
                .Sum(s => (s.Quantity * s.Profit));
            var expenditure = expenditures
                .Where(e => e.Date.Date.WithinThisMonth())
                .Sum(e => e.Amount);
            return new DashboardSummary
            {
                SummaryType = SummaryType.Month,
                ItemsSold = (uint) itemsSold,
                Sales = (ulong) totalSales,
                Expenditure = (ulong) expenditure,
                Profit = (ulong) (profit - expenditure)
            };
        }

        public DashboardSummary GetYearSummary(List<Sale> sales, List<Expenditure> expenditures)
        {
            var itemsSold = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisYear())
                .Sum(s => s.Quantity);
            var totalSales = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisYear())
                .Sum(s => (s.Quantity * s.Price));
            var profit = sales
                .Where(s => s.SaleInvoice.Date.Date.WithinThisYear())
                .Sum(s => (s.Quantity * s.Profit));
            var expenditure = expenditures
                .Where(e => e.Date.Date.WithinThisYear())
                .Sum(e => e.Amount);
            return new DashboardSummary
            {
                SummaryType = SummaryType.Year,
                ItemsSold = (uint) itemsSold,
                Sales = (ulong) totalSales,
                Expenditure = (ulong) expenditure,
                Profit = (ulong) (profit - expenditure)
            };
        }
    }
}
