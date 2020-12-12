using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Shopper.Mvc.ViewModels
{
    public class ImagesUploadModel
    {
        public string CurrentImageName { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> Images { get; set; }
        public short ImagesCount { get; set; }
    }
}
