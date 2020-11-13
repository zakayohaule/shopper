using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shopper.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var viewName = context.RouteData.Values["action"] as string;
            
            if (!context.ModelState.IsValid)
            {
                context.Result = new ViewResult
                {
                    ViewName = viewName
                };
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}