using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Database
{
    public class TenantDbContext : BaseDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantDbContext(DbContextOptions<TenantDbContext> options,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppUser).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                base.OnConfiguring(optionsBuilder);
                
                var dbName = _httpContextAccessor.HttpContext.Items["db"].ToString();
                var connectionString = "";
                if (string.IsNullOrEmpty(dbName))
                {
                    connectionString = _configuration.GetConnectionString("Default");
                }
                else
                {
                    var con = _configuration.GetConnectionString("Tenant");
                    connectionString = con.Replace("{dbName}", dbName);
                }

                optionsBuilder.UseMySql(connectionString);
            }
            catch (ArgumentException e)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new Exception($"Could not connect to the specified tenant's database: {e.Message}");
            }
        }
    }
}