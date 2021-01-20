using System;
using System.Threading.Tasks;
using Shopper.Mvc.Entities;
using Shopper.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface IBusinessService
    {
        Task<bool> IsDuplicateAsync(string name, Guid id);
        Task UpdateBusinessInfo(BusinessInfoModel formModel, Tenant getCurrentTenant);
    }
}
