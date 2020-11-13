using Microsoft.AspNetCore.Mvc.Filters;

namespace Shopper.Attributes
{
    public class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
    }
}