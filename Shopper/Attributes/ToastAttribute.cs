using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shopper.Attributes
{
    public class ToastAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            var tempData = controller?.TempData;
            if (tempData != null && tempData.Keys.Contains("Toast"))
            {
                controller.TempData["Toast"] = tempData["Toast"];
            }
            base.OnActionExecuted(context);
        }
    }
}
