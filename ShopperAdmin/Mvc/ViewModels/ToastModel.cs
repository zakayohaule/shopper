using ShopperAdmin.Mvc.Enums;

namespace ShopperAdmin.Mvc.ViewModels
{
    public class ToastModel
    {
        public ToastType Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
