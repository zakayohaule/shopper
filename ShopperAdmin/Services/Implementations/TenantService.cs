using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopperAdmin.Database;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.Entities.Identity;
using ShopperAdmin.Mvc.Entities.Tenants;
using ShopperAdmin.Mvc.ViewModels;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TenantDbContext _tenantDbContext;
        private readonly IConfiguration _configuration;

        public TenantService(ApplicationDbContext dbContext, TenantDbContext tenantDbContext,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _tenantDbContext = tenantDbContext;
            _configuration = configuration;
        }

        public async Task<Tenant> FindByIdAsync(Guid id)
        {
            return await _dbContext.Tenants.FindAsync(id);
        }

        public IQueryable<Tenant> GetTenantAsQueryable()
        {
            return _dbContext.Tenants.AsQueryable();
        }

        public async Task<Tenant> CreateAsync(CreateTenantModel formModel)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                var tenant = new Tenant
                {
                    Active = true,
                    Code = formModel.Code,
                    Domain = formModel.Domain,
                    Name = formModel.Name,
                    ConnectionString = formModel.ConnectionString,
                };

                tenant = _dbContext.Tenants.Add(tenant).Entity;
                await _dbContext.SaveChangesAsync();

                _tenantDbContext.ConnectionString = tenant.ConnectionString;
                await _tenantDbContext.Database.BeginTransactionAsync();
                var tenantTenant = _tenantDbContext.Tenants.Add(tenant).Entity;
                await _tenantDbContext.SaveChangesAsync();
                _tenantDbContext.Database.CommitTransaction();
                _dbContext.Database.CommitTransaction();

                var client = await GetTenantAppClientAsync();
                var tenantUrl = _configuration.GetValue<string>("TenantUrl").Replace("{sub}", tenantTenant.Domain);
                var response = await client.PostAsync($"{tenantUrl}/create-tenant-user",
                    new StringContent(JsonConvert.SerializeObject(formModel),
                        Encoding.Default,
                        "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    _dbContext.Tenants.Remove(tenant);
                    _tenantDbContext.Tenants.Remove(tenantTenant);
                    await _dbContext.SaveChangesAsync();
                    await _tenantDbContext.SaveChangesAsync();
                    var content = await response.Content.ReadAsStringAsync();
                    throw new Exception(content);
                }

                return tenant;
            }
            catch (Exception e)
            {
                if (_tenantDbContext.Database.CurrentTransaction != null)
                {
                    _tenantDbContext.Database.RollbackTransaction();
                }

                if (_dbContext.Database.CurrentTransaction != null)
                {
                    _dbContext.Database.RollbackTransaction();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteAsync(Tenant tenant)
        {
            _tenantDbContext.ConnectionString = tenant.ConnectionString;
            _tenantDbContext.Remove(tenant);
            _dbContext.Tenants.Remove(tenant);
            await _tenantDbContext.SaveChangesAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<HttpClient> GetTenantAppClientAsync()
        {
            var httpClient = new HttpClient();
            var tenantUrl = _configuration.GetValue<string>("TenantUrl").Replace("{sub}.", "");
            var disco = await httpClient.GetDiscoveryDocumentAsync(tenantUrl);
            if (disco.IsError)
            {
                return null;
            }

            // request token
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration.GetSection("ClientDetails")["ClientId"],
                ClientSecret = _configuration.GetSection("ClientDetails")["Password"],
                Scope = "add_user"
            });

            if (tokenResponse.IsError)
            {
                return null;
            }

            httpClient.SetBearerToken(tokenResponse.AccessToken);
            return httpClient;
        }

        public async Task<bool> CreateTenantUserAndRole(Tenant tenant, CreateTenantModel formModel)
        {
            var client = await GetTenantAppClientAsync();


            return true;
        }
    }
}
