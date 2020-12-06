using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetAllProducts();
        public Task<Product> FindByIdWithAttributesAsync(uint id);
        public Task<Product> FindProductSkusAsync(uint productId);
        List<SelectListItem> GetProductsSelectListItems();
        List<Product> GetStockedProducts();
        public bool IsDuplicate(string name, uint id);
        public Task<bool> ExistsByIdAsync(uint id);
        Task<Product> CreateProductAsync(Product newProduct, string[] attributes = null);

        Task<Sku> AddProductToStockAsync(Sku sku, List<ushort> attributeOptions);
        Task<bool> HasAttributes(uint productId, List<ushort> attributes);
    }
}
