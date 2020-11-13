using System;
using System.Threading.Tasks;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Shopper.Attributes
{
    public class PermissionsAttribute : Attribute, IAuthorizationFilter
    {
        public PermissionsAttribute(string permissions)
        {
            Permissions = permissions;
        }

        private string Permissions { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userPrincipal = context.HttpContext.User;

            if (!userPrincipal.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            else
            {
                var permissions = Permissions.Split(",", StringSplitOptions.RemoveEmptyEntries);
            
                foreach (var permission in permissions)
                {
                    if (!context.HttpContext.HasPermission(permission))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                } 
            }

        }
    }
}