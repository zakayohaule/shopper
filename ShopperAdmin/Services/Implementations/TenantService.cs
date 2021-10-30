using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopperAdmin.Database;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.ViewModels;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TenantDbContext _tenantDbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TenantService> _logger;

        public TenantService(ApplicationDbContext dbContext, TenantDbContext tenantDbContext,
            IConfiguration configuration, ILogger<TenantService> logger)
        {
            _dbContext = dbContext;
            _tenantDbContext = tenantDbContext;
            _configuration = configuration;
            _logger = logger;
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
                _logger.LogWarning("********* Starting Tenant Db Transaction ************");
                await _dbContext.Database.BeginTransactionAsync();
                var tenant = new Tenant
                {
                    Active = true,
                    Code = formModel.Code,
                    Domain = formModel.Domain,
                    Name = formModel.Name,
                    ConnectionString = formModel.ConnectionString,
                };

                _logger.LogWarning("********* Adding Tenant In Admin App ************");
                tenant = _dbContext.Tenants.Add(tenant).Entity;
                await _dbContext.SaveChangesAsync();

                _tenantDbContext.ConnectionString = tenant.ConnectionString;
                _logger.LogWarning("********* Starting Tenant Db Transaction ************");
                await _tenantDbContext.Database.BeginTransactionAsync();

                _logger.LogWarning("********* Adding Tenant In Tenant App ************");
                var tenantTenant = _tenantDbContext.Tenants.Add(tenant).Entity;
                await _tenantDbContext.SaveChangesAsync();

                _tenantDbContext.Database.CommitTransaction();
                _dbContext.Database.CommitTransaction();

                var client = await GetTenantAppClientAsync(tenantTenant);
                var tenantUrl = _configuration.GetValue<string>("TenantUrl").Replace("{sub}", tenantTenant.Domain);
                _logger.LogWarning($"********* This is the tenant URL = {tenantUrl} ************");
                _logger.LogWarning($"********* Sending post request to the create tenant user url in tenant app ************");
                var response = await client.PostAsync($"{tenantUrl}/create-tenant-user",
                    new StringContent(JsonConvert.SerializeObject(formModel),
                        Encoding.Default,
                        "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"********* Request was unsuccessful ************");
                    _dbContext.Tenants.Remove(tenant);
                    _tenantDbContext.Tenants.Remove(tenantTenant);
                    await _dbContext.SaveChangesAsync();
                    await _tenantDbContext.SaveChangesAsync();
                    var content = await response.Content.ReadAsStringAsync();
                    throw new Exception(content);
                }

                _logger.LogWarning($"********* Request was successful, Return tenant ************");
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

        public async Task<HttpClient> GetTenantAppClientAsync(Tenant tenant)
        {
            var httpClient = new HttpClient();
            var tenantUrl = _configuration.GetValue<string>("TenantUrl").Replace("{sub}.", "");
            _logger.LogWarning($"*************** This is the tenant base url: {tenantUrl} ***************");
            var disco = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = tenantUrl,
                Policy =
                {
                    RequireHttps = false
                }
            });
            if (disco.IsError)
            {
                _logger.LogError($"*************** {disco.ErrorType} : {disco.Error} ***************");
                _logger.LogError($"*************** There was an error when fetching the discovery document ***************");
                throw new ArgumentNullException("Could not fetch the discovery document from the tenant app");
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
                _logger.LogError($"*************** {tokenResponse.Error}: {tokenResponse.ErrorDescription} ***************");
                _logger.LogError($"*************** There was an error when fetching the access token ***************");
                throw new ArgumentNullException("Could not obtain access token from the tenant app");
            }

            _logger.LogWarning($"*************** {tokenResponse.AccessToken} ***************");
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            return httpClient;
        }

        public async Task<bool> CreateTenantUserAndRole(Tenant tenant, CreateTenantModel formModel)
        {
            var client = await GetTenantAppClientAsync(tenant);
            return true;
        }
    }
}
