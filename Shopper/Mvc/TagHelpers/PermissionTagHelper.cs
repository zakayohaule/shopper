using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Shopper.Extensions.Helpers;

namespace Shopper.Mvc.TagHelpers
{
    [HtmlTargetElement("permission")]
    public class PermissionTagHelper : TagHelper
    {
        [ViewContext] public ViewContext ViewContext { get; set; }

        public string Name { get; set; }

        [CanBeNull] public bool? Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Console.WriteLine($"Permission name: {Name}   Condition: {Condition}");
            if (Condition == null)
            {
                Condition = true;
            }

            output.TagName = "";
            if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!(bool)Condition)
                {
                    output.SuppressOutput();
                    return;
                }
                var hasPermission = ViewContext.HttpContext.HasPermission(Name);
                if (!hasPermission)
                {
                    output.SuppressOutput();
                }
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}