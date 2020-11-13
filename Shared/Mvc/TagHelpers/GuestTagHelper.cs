using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shared.Mvc.TagHelpers
{
    public class GuestTagHelper : TagHelper
    {
        [ViewContext] public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
            }
        }
    }
}