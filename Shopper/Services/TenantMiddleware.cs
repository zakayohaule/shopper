using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shopper.Common;

namespace Shopper.Services
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TenantResolver tenantResolver)
        {
            var tenant = tenantResolver.Resolve(context);
            if (tenant == null)
            {
                throw new InvalidTenantException("Invalid tenant");
            }
            context.Items.Add("tenant", tenant);
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
