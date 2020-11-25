using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IAttributeService
    {
        IQueryable<Attribute>  GetAllAttributes();
        List<SelectListItem> GetAllAttributeSelectListItems();
        Task<Attribute> FindByNameAsync(string name);
        Task<Attribute> FindByIdAsync(ushort id);
        Task<Attribute> CreateAsync(Attribute productGroup);
        Task<Attribute> UpdateAsync(Attribute productGroup);
        Task DeleteAttributeAsync(Attribute productGroup);

        bool IsDuplicate(Attribute attribute);
        public bool IsDuplicate(string name, ushort id);
        public Task<bool> ExistsById(ushort id);
    }
}
