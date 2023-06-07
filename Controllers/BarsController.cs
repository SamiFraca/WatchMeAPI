using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Services;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;


namespace WatchMe.Controllers
{
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    [Route("[controller]")]
    public class BarsController : ControllerBase
    {
        private readonly BarService _barService;
        private readonly DataContext _dbContext;

        public BarsController(
            BarService barService,
            DataContext dbContext
        )
        {
            _barService = barService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBarsAsync()
        {
            var bars = await _barService.GetBarsAsync();
            if (bars == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(bars);
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetBarsFromUser(int UserId)
        {
            var barsUser = await _barService.GetBarsFromUser(UserId);
            if (barsUser == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(barsUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarAsync(int id)
        {
            var bar = await _barService.GetBarAsync(id);
            if (bar == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(bar);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> SearchLocation(string location)
        {
            return await _barService.SearchLocation(location);
        }

        [HttpGet("names")]
        public async Task<IActionResult> SearchName(string name)
        {
            return await _barService.SearchName(name);
        }

        [HttpGet("shows/sports")]
        public async Task<IActionResult> SearchSport(string sport)
        {
            return await _barService.SearchSport(sport);
        }

        /// <summary>
        /// Creates a new bar.
        /// </summary>
        /// <param name="imageFile">The image file to upload.</param>
        /// <param name="bar">The bar data.</param>
        /// <returns>The created bar.</returns>
        [HttpPost]
        public async Task<IActionResult> PostBarAsync(
            IFormFile? imageFile,
            string name,
            string location,
            int capacity,
            string description,
            int userId
        )
        {
            return await _barService.PostBarAsync(
                imageFile,
                name,
                location,
                capacity,
                description,
                userId
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarAsync(int id, Bar Bar)
        {
            return await _barService.PutBarAsync(id, Bar);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarAsync(int id)
        {
            return await _barService.DeleteBarAsync(id);
        }
    }
}
