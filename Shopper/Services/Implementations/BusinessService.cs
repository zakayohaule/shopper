using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopper.Database;
using Shopper.Mvc.Entities;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class BusinessService : IBusinessService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AdminAppDbContext _appDbContext;
        private readonly IFileUploadService _fileUploadService;

        public BusinessService(ApplicationDbContext dbContext, IFileUploadService fileUploadService, AdminAppDbContext appDbContext)
        {
            _dbContext = dbContext;
            _fileUploadService = fileUploadService;
            _appDbContext = appDbContext;
        }

        public Task<bool> IsDuplicateAsync(string name, Guid id)
        {
            return _dbContext.Tenants.AnyAsync(t =>
                t.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && t.Id != id);
        }

        public async Task UpdateBusinessInfo(BusinessInfoModel formModel, Tenant tenant)
        {
            tenant.Name = formModel.Name;
            tenant.Description = formModel.Description;
            tenant.Email = formModel.Email;
            tenant.Address = formModel.Address;
            tenant.PhoneNumber1 = formModel.Phone1;
            tenant.PhoneNumber2 = formModel.Phone2;

            if (formModel.Image != null)
            {
                var logoPath = await _fileUploadService.UploadTenantLogo(formModel.Image);
                tenant.LogoPath = logoPath;
            }
            _dbContext.Tenants.Update(tenant);
            _appDbContext.Tenants.Update(tenant);
            await _dbContext.SaveChangesAsync();
            await _appDbContext.SaveChangesAsync();
        }
    }
}
