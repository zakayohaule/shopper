using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IExpenditureTypeService
    {
        IEnumerable<ExpenditureType>  GetAllExpenditureTypes();
        List<SelectListItem>  GetExpenditureTypeSelectListItems();
        Task<ExpenditureType> FindByNameAsync(string name);
        Task<ExpenditureType> FindByIdAsync(ushort id);
        Task<ExpenditureType> CreateAsync(ExpenditureType expenditureType);
        Task<ExpenditureType> UpdateAsync(ExpenditureType expenditureType);
        Task DeleteExpenditureTypeAsync(ExpenditureType expenditureType);

        bool IsDuplicate(ExpenditureType expenditureType);
        public bool IsDuplicate(string name, ushort id);
    }
}
