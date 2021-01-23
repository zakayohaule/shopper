using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shopper.Mvc.Entities;

namespace Shopper.Mvc.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {

        public BreadcrumbViewComponent()
        {

        }

        public IViewComponentResult Invoke(string filter)
        {
            if (ViewBag.Breadcrumb == null)
            {
                ViewBag.Breadcrumb = new List<Message>();
            }

            return View(ViewBag.Breadcrumb as List<Message>);
        }
    }
}
