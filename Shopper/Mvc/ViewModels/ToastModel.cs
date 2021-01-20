using Shopper.Mvc.Enums;

namespace Shopper.Mvc.ViewModels
{
    public class ToastModel
    {
        public ToastType Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
