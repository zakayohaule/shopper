using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentEmail.Core;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

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
