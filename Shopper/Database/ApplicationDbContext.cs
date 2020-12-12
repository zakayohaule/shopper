using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.Entities;
using Shopper.Services.Interfaces;
using Attribute = Shared.Mvc.Entities.Attribute;

namespace Shopper.Database
{
    public class ApplicationDbContext : IdentityDbContext<
        AppUser, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
            : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        //Identity entities
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<AppUser> Users { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }


        //custom entities
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeOption> AttributeOptions { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuAttribute> SkuAttributes { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<SkuSellingPrice> SkuSellingPrices { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.EnableDetailedErrors();
            try
            {
                base.OnConfiguring(optionsBuilder);
                var dbName =  _tenantService.GetCurrentTenant();
                var con = _configuration.GetConnectionString("Default");
                var connectionString = con.Replace("{dbName}", dbName);
                optionsBuilder.UseMySql(connectionString);
            }
            catch (ArgumentException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Could not connect to the specified tenant's database: {e.Message}");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppUser).Assembly);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
