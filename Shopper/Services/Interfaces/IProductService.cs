using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shopper.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetAllProducts();
        public Task<Product> FindByIdWithAttributesAsync(uint id);
        public Task<Product> FindProductSkusAsync(uint productId);
        List<SelectListItem> GetAttributeGroupings();
        List<SelectListItem> GetProductsSelectListItems();
        public List<SelectListItem> GetProductsSelectListItemsForSale();
        List<Product> GetStockedProducts();
        public bool IsDuplicate(Product product);
        public bool IsDuplicate(string name, uint id);
        public Task<bool> ExistsByIdAsync(uint id);
        Task<Product> FindByIdAsync(uint id);
        Task<Product> CreateProductAsync(Product newProduct, string[] attributes = null);

        Task<Sku> AddProductToStockAsync(Sku sku, List<ushort> attributeOptions);
        Task<bool> HasAttributes(uint productId,List<ushort> attributes);
    }

}
