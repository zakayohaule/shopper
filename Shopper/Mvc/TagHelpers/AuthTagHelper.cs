using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shopper.Mvc.TagHelpers
{
    public class AuthTagHelper : TagHelper
    {
        [ViewContext] public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            if (!ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
            }
        }
    }
}
