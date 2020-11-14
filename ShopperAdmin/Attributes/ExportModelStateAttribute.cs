using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopperAdmin.Extensions.Helpers;

namespace ShopperAdmin.Attributes
{
    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState = filterContext.ModelState;
            //Only export when ModelState is not valid
            if (!modelState.IsValid)
            {
                //Export if we are redirecting
                if (filterContext.Result is RedirectResult 
                    || filterContext.Result is RedirectToRouteResult 
                    || filterContext.Result is RedirectToActionResult)
                {
                    var controller = filterContext.Controller as Controller;
                    if (controller != null && filterContext.ModelState != null)
                    {
                        var serialisedModelState = modelState.SerializeModelStater();
                        controller.TempData[Key] = serialisedModelState;
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }

        /*public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Only export when ModelState is not valid
            if (!filterContext.ModelState.IsValid)
            {
                Console.WriteLine($"************* invalid model state {Key}  ***************");
                //Export if we are redirecting
                if (filterContext.Result is RedirectResult || filterContext.Result is RedirectToRouteResult || filterContext.Result is RedirectToActionResult)
                {
                    Console.WriteLine($"************* is a redirect result => {Key} ***************");
                    // filterContext.ModelState.Root.ToList().ForEach(ms =>
                    // {
                    //     ((Controller) filterContext.Controller).TempData[Key] = ;
                    // });
                    ((Controller) filterContext.Controller).TempData[Key] = JsonConvert.SerializeObject(filterContext.ModelState);
                }
            }

            base.OnActionExecuted(filterContext);
        }*/
    }
}