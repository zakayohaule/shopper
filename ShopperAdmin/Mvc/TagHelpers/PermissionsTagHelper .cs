using System;
using System.Linq;
using ShopperAdmin.Extensions.Helpers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ShopperAdmin.Mvc.TagHelpers
{
    [HtmlTargetElement("permissions")]
    public class PermissionsTagHelper : TagHelper
    {
        [ViewContext] public ViewContext ViewContext { get; set; }

        public string Names { get; set; }

        [CanBeNull] public bool? Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Condition ??= true;

            output.TagName = "";
            if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!(bool) Condition)
                {
                    output.SuppressOutput();
                    return;
                }

                var permissions = Names.Split(",", StringSplitOptions.RemoveEmptyEntries);

                if (permissions.Any(permission => !ViewContext.HttpContext.HasPermission(permission)))
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