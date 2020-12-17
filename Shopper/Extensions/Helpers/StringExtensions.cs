using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Shopper.Extensions.Helpers
{
    public static class StringExtensions
    {
        public static string LoadProductImage(this string imageName, HttpContext context)
        {
            var tenant = context.GetCurrentTenant();
            if (tenant == null)
            {
                return "kea";
            }
            return $"/uploads/products/{tenant.Domain.Split(".")[0]}/{imageName}";
        }
    }
}
