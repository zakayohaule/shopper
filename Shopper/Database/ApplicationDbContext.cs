using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.BaseEntities;
using Shared.Mvc.Entities.Identity;
using Shopper.Extensions.Configurations;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;
using Attribute = Shared.Mvc.Entities.Attribute;
using Module = Shared.Mvc.Entities.Identity.Module;

namespace Shopper.Database
{
    public class ApplicationDbContext : IdentityDbContext<
        AppUser, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantIdentifierService _tenantIdentifierService;

        public Tenant Tenant { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration,
            ITenantIdentifierService httpContextAccessor, IHttpContextAccessor httpContextAccessor1, ITenantIdentifierService tenantIdentifierService)
            : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor1;
            _tenantIdentifierService = tenantIdentifierService;
            if (Tenant == null)
            {
                Tenant = httpContextAccessor.GetTenantFromRequest();
            }
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
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeOption> AttributeOptions { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuAttribute> SkuAttributes { get; set; }
        public DbSet<Expiration> Expirations { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<SkuSellingPrice> SkuSellingPrices { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }
        public DbSet<ExpenditureType> ExpenditureTypes { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                var connectionString = "";
                base.OnConfiguring(optionsBuilder);
                if (Tenant.ConnectionString.IsNullOrEmpty())
                {
                    connectionString = _configuration.GetConnectionString("Default").Replace("{dbName}", "shopper");
                    /*var tenantConnectionString = _tenantService.GetCurrentTenantConnectionString();

                    connectionString = tenantConnectionString.IsNullOrEmpty()
                        ? _configuration.GetConnectionString("Default").Replace("{dbName}", "shopper")
                        : tenantConnectionString;*/
                }
                else
                {
                    connectionString = Tenant.ConnectionString;
                }

                optionsBuilder.UseMySql(connectionString);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not connect to the specified tenant's database: {e.Message}");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Console.WriteLine("******************* This method is being called now! *******************");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppUser).Assembly);
            builder.Entity<AppUser>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Role>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Attribute>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<AttributeOption>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Expenditure>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<ExpenditureType>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<PriceType>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Product>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Sale>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<SaleInvoice>().HasQueryFilter(e => e.TenantId == Tenant.Id);
            builder.Entity<Sku>().HasQueryFilter(e => e.TenantId == Tenant.Id);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetTenantId();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetTenantId();
            return base.SaveChanges();
        }

        private void SetTenantId()
        {
            foreach (var entityEntry in ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added)
            ) // Iterate all made changes
            {
                if (entityEntry.Properties.Any(entry => entry.Metadata.Name.Equals("TenantId")))
                {
                    var tenantIdProp = entityEntry.Property("TenantId");
                    var tenant = Tenant ?? _tenantIdentifierService.GetTenantFromRequest();
                    tenantIdProp.CurrentValue = tenant.Id;
                    // entityEntry.Property("TenantId").CurrentValue = _tenantService.GetTenantFromRequest().Id;
                }
            }
        }
    }
}
