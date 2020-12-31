using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IProductTypeService
    {
        IQueryable<ProductType> GetAllProductTypes();
        List<SelectListItem> GetProductTypeSelectListItems();
        List<SelectListItem> GetProductTypeSelectListItemsByCategoryId(ushort categoryId, ushort? selectedTypeId);
        Task<ProductType> FindByNameAsync(string name);
        Task<ProductType> FindByIdAsync(ushort id);
        Task<ProductType> CreateAsync(ProductType productType);
        Task<ProductType> UpdateAsync(ProductType productType);
        Task DeleteProductTypeAsync(ProductType productType);
        bool IsDuplicate(ProductType productType);
        bool IsDuplicate(string name, ushort id);
        Task<bool> ExistsByIdAsync(ushort id);
    }
}
