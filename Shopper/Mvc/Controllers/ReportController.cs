using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Authorize, Route("reports")]
    public class ReportController : Controller
    {
        [HttpGet("sales")]
        public async Task<IActionResult> Sales([FromServices] ISaleService saleService)
        {
            var sales = await saleService.GetSalesReportAsync();

            return View(sales);
        }
    }
}
