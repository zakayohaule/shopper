﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IProductCategoryService
    {
        IQueryable<ProductCategory> GetAllProductCategories();
        List<SelectListItem> GetProductCategorySelectListItems();
        Task<ProductCategory> FindByNameAsync(string name);
        Task<ProductCategory> FindByIdAsync(ushort id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<ProductCategory> UpdateAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(ProductCategory productCategory);
        bool IsDuplicate(ProductCategory productCategory);
        bool IsDuplicate(string name, ushort id);
        Task<bool> ExistsByIdAsync(ushort id);
    }
}
