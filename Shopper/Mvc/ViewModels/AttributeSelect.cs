using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopper.Mvc.ViewModels
{
    public class AttributeSelect
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> Options { get; set; }
    }
}
