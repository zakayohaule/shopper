﻿using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Shopper.Mvc.ViewComponents
{
    public class MenuUserViewComponent : ViewComponent
    {

        public MenuUserViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
