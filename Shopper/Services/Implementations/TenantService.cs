using IdentityServer4.Extensions;
using IdentityServer4.Stores.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TenantService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string GetCurrentTenant()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return _configuration.GetValue<string>("DefaultTenantDb");
            }
            if (!httpContext.User.IsAuthenticated())
            {
                return GetTenantFromSubDomain();
            } 
            return GetTenantFromSubDomain();
        }

        private string GetTenantFromSubDomain()
        {
            var subDomain = string.Empty;
            var host = _httpContextAccessor.HttpContext.Request.Host.Host;

            if (!string.IsNullOrWhiteSpace(host))
            {
                if (host.Contains("."))
                {
                    subDomain = host.Split('.')[0];
                }
            }

            if (string.IsNullOrEmpty(subDomain))
            {
                subDomain = _configuration.GetValue<string>("DefaultTenantDb");
            }
            return subDomain.Trim().ToLower();
        }
    }
}