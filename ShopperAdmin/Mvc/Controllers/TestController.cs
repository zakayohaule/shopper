using Microsoft.AspNetCore.Mvc;
using ShopperAdmin.Database;

namespace ShopperAdmin.Mvc.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public TestController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("test-db-context")]
        public IActionResult ListUsers()
        {
            return Ok(_dbContext.Users);
        }
    }
}