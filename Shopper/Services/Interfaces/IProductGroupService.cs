using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IProductGroupService
    {
        IEnumerable<ProductGroup>  GetAllProductGroups();
        List<SelectListItem>  GetProductGroupSelectListItems();
        Task<ProductGroup> FindByNameAsync(string name);
        Task<ProductGroup> FindByIdAsync(ushort id);
        Task<ProductGroup> CreateAsync(ProductGroup productGroup);
        Task<ProductGroup> UpdateAsync(ProductGroup productGroup);
        Task DeleteProductGroupAsync(ProductGroup productGroup);

        bool IsDuplicate(ProductGroup productGrpGroup);
        public bool IsDuplicate(string name, ushort id);
    }
}
