using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
                }
            }
            return Json(new { success = true });
        }

        [HttpPut]
        public async Task<Bar> UpdateImage(IFormFile file, int id)
        {
            if (file != null && file.Length > 0 && id != 0)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    string fileName = file.FileName;
                    return await _blobStorageService.UpdateImageAsync(fileName, stream,id);
                }
            }
            throw new ArgumentException("Invalid file or ID provided.");
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
