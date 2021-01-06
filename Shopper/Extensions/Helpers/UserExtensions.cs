using System;
 using System.IO;
 using System.Linq;
 using System.Security.Claims;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc.Rendering;
 using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.Hosting;
 using Microsoft.IdentityModel.JsonWebTokens;
 using Shared.Common;
 using Shared.Extensions.Helpers;
 using Shared.Mvc.Entities;
 using Shopper.Services.Interfaces;

 namespace Shopper.Extensions.Helpers
{
    public static class UserExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaim = claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            if (userIdClaim.IsNull())
            {
                userIdClaim = claimsPrincipal.Claims
                    .FirstOrDefault(claim => claim.Type == "sub")
                    ?.Value;
            }
            if (userIdClaim == null) return -1;
            long.TryParse(userIdClaim, out var userId);
            return userId;
        }

        public static long GetUserId(this HttpContext httpContext)
        {
            return GetUserId(httpContext.User);
        }

        public static bool HasPermission(this HttpContext httpContext, string permission)
        {
            var userClaimService = httpContext.RequestServices.GetRequiredService<IUserClaimService>();
            return userClaimService.HasPermission(httpContext.GetUserId(), permission);
        }

        public static uint GetInstitutionId(this ClaimsPrincipal claimsPrincipal)
        {
            var institutionId = claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == "InstitutionId")
                ?.Value;
            return institutionId == null ? (uint) 0 : uint.Parse(institutionId);
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var fullName = claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == "Email")
                ?.Value;
            return fullName ?? string.Empty;
        }

        public static string GetUserFullName(this ClaimsPrincipal claimsPrincipal)
        {
            var fullName = claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == "FullName")
                ?.Value;
            return fullName ?? string.Empty;
        }

        public static string GetBearerToken(this HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"];

            if (authorizationHeader.IsNull())
            {
                Console.WriteLine("No Authorization header");
                return string.Empty;
            }

            var bearerHeader = authorizationHeader.FirstOrDefault();

            if (bearerHeader.IsNull())
            {
                Console.WriteLine("Authorization header doesn't contain a bearer token");
                return string.Empty;
            }

            if (!bearerHeader.Contains("Bearer"))
            {
                Console.WriteLine("Authorization header doesn't contain a bearer token");
                return string.Empty;
            }

            Console.WriteLine("Getting the access token from the authorization header");
            return bearerHeader.Remove(0,7);
        }

        public static string GetClientIdFromToken(this HttpContext httpContext)
        {
            var token = httpContext.GetBearerToken();
            var tokenHandler = new JsonWebTokenHandler();
            var clientId = tokenHandler.ReadJsonWebToken(token);
            return clientId.Claims.First(claim => claim.Type == "client_id").Value;
        }

        public static string GetClientIdFromToken(this string accessToken)
        {
            var tokenHandler = new JsonWebTokenHandler();
            try
            {
                var clientId = tokenHandler.ReadJsonWebToken(accessToken);
                return clientId.Claims.First(claim => claim.Type == "client_id").Value;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not decode client token");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public static string AppBaseUrl(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }

        public static string GetEmailTemplate(this IHostEnvironment hostEnvironment, string templateName)
        {
            try
            {
                var path = Path.GetFullPath(Path.Combine(hostEnvironment.ContentRootPath,
                    $"Mvc/Views/Emails/{templateName}.cshtml"));
                return path;
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw new FileNotFoundException($"Email template '{templateName}' could not be found in the Mvc/Views/Emails directory");
            }

        }

        public static string GetTenantFromSubdomain(this HttpContext httpContext)
        {
            var subDomain = string.Empty;
            var host = httpContext.Request.Host.Host;

            if (string.IsNullOrWhiteSpace(host)) return subDomain;
            if (host.Contains("."))
            {
                var domainParts = host.Split(".");
                                if (domainParts.Length == 2)
                                {
                                    subDomain = domainParts[0];
                                }
                                
                                if (domainParts.Length == 3)
                                {
                                    subDomain = string.Join(".", domainParts[..2]);
                                }
                
                                if (domainParts.Length == 4)
                                {
                                    subDomain = string.Join(".", domainParts[1..3]);
                                }
            }

            return subDomain;
        }

        public static Tenant GetCurrentTenant(this ViewContext viewContext)
        {
            var tenant = (Tenant)viewContext.HttpContext.Items["tenant"];
            return tenant;
        }

        public static Tenant GetCurrentTenant(this HttpContext httpContext)
        {
            return (Tenant) httpContext.Items["tenant"];
        }
    }
}
