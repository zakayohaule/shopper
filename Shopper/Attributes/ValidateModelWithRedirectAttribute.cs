using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shopper.Attributes
{
    public class ValidateModelWithRedirectAttribute : ActionFilterAttribute
    {
        private string Controller { get; }
        private string Action { get; }
        public object RouteValues { get; }

        public ValidateModelWithRedirectAttribute(string controller, string action, Object routeValues)
        {
            Controller = controller;
            Action = action;
            RouteValues = routeValues;
        }
        
        
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                // Console.WriteLine($"{context.RouteData}");
                // context.Result = new RedirectToActionResult(Action,Controller,RouteValues);
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}