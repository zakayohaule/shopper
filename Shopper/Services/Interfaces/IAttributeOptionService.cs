using System.Linq;
using System.Threading.Tasks;
using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface IAttributeOptionService
    {
        IQueryable<AttributeOption> GetAllAttributeOptions();
        IQueryable<AttributeOption> GetAllAttributeOptionsByAttribute(ushort attributeId);
        Task<AttributeOption> FindByNameAsync(string name);
        Task<AttributeOption> FindByIdAsync(ushort id);
        Task<AttributeOption> CreateAsync(AttributeOption productCategory);
        Task<AttributeOption> UpdateAsync(AttributeOption productCategory);
        Task DeleteAttributeOptionAsync(AttributeOption productCategory);
        bool IsDuplicate(AttributeOption productCategory);
        bool IsDuplicate(string name, ushort id);
        Task<bool> ExistsByIdAsync(ushort id);
    }
}
