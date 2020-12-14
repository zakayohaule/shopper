using System;
using Microsoft.AspNetCore.Mvc;

namespace Shopper.Mvc.ViewComponents
{
    public class PageHeaderViewComponent : ViewComponent
    {

        public PageHeaderViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            Tuple<string, string> message;

            if (ViewBag.PageHeader == null)
            {
                message = Tuple.Create(ViewData["Title"]?.ToString(), string.Empty);
            }
            else
            {
                message = ViewBag.PageHeader as Tuple<string, string>;
            }
            return View(message);
        }
    }
}
