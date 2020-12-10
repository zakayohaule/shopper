using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shopper.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> FindByIdAsync(uint id);
        IQueryable<Product> FindByIdAsyncQ(uint id);
        IQueryable<Product> GetAllProducts();
        public Task<Product> FindByIdWithAttributesAsync(uint id);
        public Task<Product> FindProductSkusAsync(uint productId);
        List<SelectListItem> GetProductsSelectListItems();
        List<Product> GetStockedProducts();
        public bool IsDuplicate(string name, uint id);
        public Task<bool> ExistsByIdAsync(uint id);
        Task<Product> CreateProductAsync(ProductFormModel newProduct);
        Task<Product> UpdateProductAsync(ProductFormModel newProduct, Product productToUpdate);

        Task<Sku> AddProductToStockAsync(Sku sku, List<ushort> attributeOptions);
        Task<bool> HasAttributes(uint productId, List<ushort> attributes);
        Task DeleteProductAsync(Product productGroup);
    }
}
