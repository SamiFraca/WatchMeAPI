using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Services;

namespace WatchMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly AzureBlobStorageService _blobStorageService;

        public ImageController(AzureBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    string fileName = file.FileName;
                    string imageUrl = await _blobStorageService.UploadImageAsync(fileName, stream);
                    // Save the imageUrl or perform additional actions
                }
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string fileName)
        {
            Stream imageStream = await _blobStorageService.GetImageAsync(fileName);

            // Return the image stream as a file response
            return File(imageStream, "image/jpeg"); 
        }
    }
}
