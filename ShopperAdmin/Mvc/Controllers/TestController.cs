using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentEmail.Core;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ShopperAdmin.Database;
using ShopperAdmin.Extensions.Helpers;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Mvc.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TenantDbContext _tenantDb;
        public TestController(ApplicationDbContext dbContext, TenantDbContext tenantDb)
        {
            _dbContext = dbContext;
            _tenantDb = tenantDb;
        }

        [HttpGet("test-db-context")]
        public IActionResult ListUsers()
        {
            var tenant = HttpContext.Request.Query["db"];
            if (string.IsNullOrEmpty(tenant))
            {
                HttpContext.Items["db"] = "kea_db";
            }
            else
            {
                HttpContext.Items["db"] = tenant;
            }
            return Ok(_tenantDb.Users);
        }
    }
}