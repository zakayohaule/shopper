using System.Linq;
using System.Threading.Tasks;
using Shopper.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IExpenditureService
    {
        IQueryable<Expenditure> GetExpenditureAsQuerable();
        Task<Expenditure> FindByIdAsync(ulong id);
        Task<Expenditure> CreateAsync(Expenditure expenditure);
        Task<Expenditure> UpdateAsync(Expenditure expenditure);
        Task DeleteExpenditureAsync(Expenditure expenditure);
        Task<bool> ExistsByIdAsync(ulong id);
    }
}
