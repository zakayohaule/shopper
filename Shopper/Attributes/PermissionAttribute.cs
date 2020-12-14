using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Attributes
{
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }

        private string Permission { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaimService = context.HttpContext.RequestServices.GetRequiredService<IUserClaimService>();
            var userPrincipal = context.HttpContext.User;

            if (!userPrincipal.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            else
            {
                if (!userClaimService.HasPermission(userPrincipal.GetUserId(), Permission))
                {
                    context.Result = new UnauthorizedResult();
                }   
            }
        }
    }
}