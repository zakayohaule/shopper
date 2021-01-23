using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopper.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IPriceTypeService
    {
        IEnumerable<PriceType>  GetAllPriceTypes();
        List<SelectListItem>  GetPriceTypeSelectListItems();
        Task<PriceType> FindByNameAsync(string name);
        Task<PriceType> FindByIdAsync(ushort id);
        Task<PriceType> CreateAsync(PriceType priceType);
        Task<PriceType> UpdateAsync(PriceType priceType);
        Task DeletePriceTypeAsync(PriceType priceType);

        bool IsDuplicate(PriceType priceType);
        public bool IsDuplicate(string name, ushort id);
    }
}
