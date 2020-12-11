using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Shopper.Extensions.Helpers
{
    public static class StringExtensions
    {
        public static string LoadProductImage(this string imageName, HttpContext context)
        {
            var tenant = context.GetTenantFromSubdomain();
            if (tenant.IsNullOrEmpty())
            {
                tenant = "kea_db";
            }
            return $"/uploads/products/{tenant}/{imageName}";
        }
    }
}
