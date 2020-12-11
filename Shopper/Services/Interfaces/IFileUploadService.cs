using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Shopper.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadProductImageAsync(IFormFile image);
    }
}
