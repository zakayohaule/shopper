using System;
using System.IO;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Shopper.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Shopper.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {

        private readonly ITenantIdentifierService _tenantIdentifierService;
        private readonly IHostEnvironment _hostEnvironment;

        public FileUploadService(ITenantIdentifierService tenantIdentifierService, IHostEnvironment hostEnvironment)
        {
            _tenantIdentifierService = tenantIdentifierService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadProductImageAsync(IFormFile formFile)
        {
            // @todo validate file type to only images

            var tenant = _tenantIdentifierService.GetTenantFromRequest();
            var webRoot = $"{_hostEnvironment.ContentRootPath}/wwwroot";
            var imageUploadPath = @$"{webRoot}/uploads/products/{tenant.Domain.Split(".")[0]}/";
            var imageThumbUploadPath = @$"{webRoot}/uploads/products/{tenant.Domain.Split(".")[0]}/thumbs";
            if (!Directory.Exists(imageUploadPath))
            {
                Directory.CreateDirectory(imageUploadPath);
            }
            if (!Directory.Exists(imageThumbUploadPath))
            {
                Directory.CreateDirectory(imageThumbUploadPath);
            }

            var image = await Image.LoadAsync(formFile.OpenReadStream());
            var thumb = await Image.LoadAsync(formFile.OpenReadStream());
            image.Mutate(x => x.Resize(500,380));
            thumb.Mutate(x => x.Resize(110,90));
            var ext = formFile.FileName.Split(".")[1];
            if (ext.IsNullOrEmpty())
            {
                ext = "jpeg";
            }
            var imageName = $"{DateTimeOffset.Now.ToUnixTimeSeconds().ToString()}.{ext}";
            await image.SaveAsync($"{imageUploadPath}/{imageName}");
            await thumb.SaveAsync($"{imageUploadPath}/thumbs/{imageName}");
            return imageName;
        }

        public async Task<string> UploadTenantLogo(IFormFile formFile)
        {
            // @todo validate file type to only images

            var tenant = _tenantIdentifierService.GetTenantFromRequest();
            var webRoot = $"{_hostEnvironment.ContentRootPath}/wwwroot";
            var imageUploadPath = @$"{webRoot}/uploads/logo/{tenant.Domain}/";
            if (!Directory.Exists(imageUploadPath))
            {
                Directory.CreateDirectory(imageUploadPath);
            }

            var image = await Image.LoadAsync(formFile.OpenReadStream());
            image.Mutate(x => x.Resize(500,380));
            var ext = formFile.FileName.Split(".")[1];
            if (ext.IsNullOrEmpty())
            {
                ext = "jpeg";
            }
            var imageName = $"{DateTimeOffset.Now.ToUnixTimeSeconds().ToString()}.{ext}";
            await image.SaveAsync($"{imageUploadPath}/{imageName}");

            image.Mutate(x => x.Resize(128,128));
            await image.SaveAsync($"{imageUploadPath}/thumb_{imageName}");

            return imageName;
        }
    }
}
