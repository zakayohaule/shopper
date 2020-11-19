using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAllProductCategories();
        Task<ProductCategory> FindByNameAsync(string name);
        Task<ProductCategory> FindByIdAsync(ushort id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<ProductCategory> UpdateAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(ProductCategory productCategory);

        bool IsDuplicate(string name, ushort id);
    }
}
