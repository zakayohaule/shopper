using Shopper.Extensions.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Shopper.Attributes
{
    public class ImportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            var serialisedModelState = controller?.TempData[Key] as string;
            var modelState = filterContext.ModelState;
            
            if (serialisedModelState != null)
            {
                //Only Import if we are viewing
                if (filterContext.Result is ViewResult)
                {
                    var deserializedModelDState = serialisedModelState.DeserializeModelState();
                    modelState.Merge(deserializedModelDState);
                }
                else
                {
                    //Otherwise remove it.
                    controller.TempData.Remove(Key);
                }
            }

            base.OnActionExecuted(filterContext);
        }

        /*public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = (Controller) filterContext.Controller;
            if (controller.TempData[Key] is ModelStateDictionary modelState)
            {
                //Only Import if we are viewing
                if (filterContext.Result is ViewResult)
                {
                    controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    //Otherwise remove it.
                    controller.TempData.Remove(Key);
                }
            }

            base.OnActionExecuted(filterContext);
        }*/
    }
}