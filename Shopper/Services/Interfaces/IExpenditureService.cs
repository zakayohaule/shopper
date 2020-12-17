using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IExpenditureService
    {
        IQueryable<Expenditure> GetAllExpenditures();
        Task<Expenditure> FindByIdAsync(ulong id);
        Task<Expenditure> CreateAsync(Expenditure expenditure);
        Task<Expenditure> UpdateAsync(Expenditure expenditure);
        Task DeleteExpenditureAsync(Expenditure expenditure);
        Task<bool> ExistsByIdAsync(ulong id);
    }
}
