using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shopper.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadProductImageAsync(IFormFile image);
    }
}
