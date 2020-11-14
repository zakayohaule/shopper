using Microsoft.AspNetCore.Mvc;

namespace ShopperAdmin.Mvc.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        public FooterViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
