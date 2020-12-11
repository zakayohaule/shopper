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

        private readonly ITenantService _tenantService;
        private readonly IHostEnvironment _hostEnvironment;

        public FileUploadService(ITenantService tenantService, IHostEnvironment hostEnvironment)
        {
            _tenantService = tenantService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadProductImageAsync(IFormFile formFile)
        {
            var tenant = _tenantService.GetCurrentTenant();
            var webRoot = $"{_hostEnvironment.ContentRootPath}/wwwroot";
            var imageUploadPath = @$"{webRoot}/uploads/products/{tenant}/";
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
            return imageName;
        }
    }
}
