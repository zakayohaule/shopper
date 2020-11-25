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
        List<SelectListItem> GetAttributeGroupings();
        public bool IsDuplicate(Product product);
        public bool IsDuplicate(string name, uint id);
        public Task<bool> ExistsByIdAsync(uint id);
        Task<Product> CreateProductAsync(Product newProduct, string[] attributes = null);
    }

}
