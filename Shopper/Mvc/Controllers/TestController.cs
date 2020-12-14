using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shopper.Database;

namespace Shopper.Mvc.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public TestController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            var permissions = _dbContext.Permissions.ToList();
            return View(permissions);
        }
    }
}
