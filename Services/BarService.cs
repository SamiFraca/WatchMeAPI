using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WatchMe.Repositories;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace WatchMe.Services
{
    public class BarService
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<BarService> _logger;
        private readonly BarRepository _barRepository;
        private readonly AzureBlobStorageService _blobStorageService;

        public BarService(
            DataContext dbContext,
            ILogger<BarService> logger,
            BarRepository barRepository,
            AzureBlobStorageService blobStorageService
        )
        {
            _dbContext = dbContext;
            _logger = logger;
            _barRepository = barRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<List<Bar>> GetBarsAsync()
        {
            return await _barRepository.GetBars();
        }

        public async Task<Bar> GetBarAsync(int id)
        {
            return await _barRepository.GetBar(id);
        }

        public async Task<List<Bar>> GetBarsFromUser(int id)
        {
            return await _barRepository.GetBarUser(id);
        }

        public async Task<IActionResult> SearchLocation(string location)
        {
            var bars = await _dbContext.Bars
                .Include(b => b.Shows)
                .Where(b => b.Location.Contains(location))
                .ToListAsync();

            if (bars.Count == 0)
            {
                return new NotFoundObjectResult($"No bars found with location: '{location}'");
            }
            return new OkObjectResult(bars);
        }

        public async Task<IActionResult> SearchName(string name)
        {
            var bars = await _dbContext.Bars
                .Include(b => b.Shows)
                .Where(b => b.Name.Contains(name))
                .ToListAsync();

            if (bars.Count == 0)
            {
                return new NotFoundObjectResult($"No bars found with name: '{name}'");
            }
            return new OkObjectResult(bars);
        }

        public async Task<IActionResult> SearchSport(string sport)
        {
            var bars = await _dbContext.Bars
                .Include(b => b.Shows)
                .Where(b => b.Shows != null && b.Shows.Any(s => s.Sport.Contains(sport)))
                .ToListAsync();

            if (bars.Count == 0)
            {
                return new NotFoundObjectResult($"No bars found with the sport: '{sport}'");
            }

            return new OkObjectResult(bars);
        }

        public async Task<IActionResult> PostBarAsync(
            IFormFile? imageFile,
            string name,
            string location,
            int capacity,
            string description,
            int userId
        )
        {
            var bar = new Bar
            {
                Name = name,
                Location = location,
                Capacity = capacity,
                Description = description,
                UserId = userId,
                Shows = null,
                ImageUrl = null
            };
            if (imageFile != null && imageFile.Length > 0)
            {
                using (Stream stream = imageFile.OpenReadStream())
                {
                    string fileName = imageFile.FileName;
                    string imageUrl = await _blobStorageService.UploadImageAsync(fileName, stream);
                    bar.ImageUrl = imageUrl;
                }
            }
            if (bar.Shows != null)
            {
                foreach (var show in bar.Shows)
                {
                    show.BarId = bar.Id;
                }
            }
            await _barRepository.PostBar(bar);
            return new CreatedAtActionResult(nameof(GetBarAsync), "Bars", new { id = bar.Id }, bar);
        }

        public async Task<IActionResult> PutBarAsync(int id, Bar bar)
        {
            if (id != bar.Id)
            {
                return new BadRequestObjectResult(null);
            }
            _dbContext.Entry(bar).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarExists(id))
                {
                    return new NotFoundObjectResult(null);
                }
                else
                {
                    throw;
                }
            }
            return new NoContentResult();
        }

        private bool BarExists(long id)
        {
            return (_dbContext.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteBarAsync(int id)
        {
            var Bar = await _dbContext.Bars.FindAsync(id);
            if (Bar == null)
            {
                return new NotFoundObjectResult(null);
            }
            var users = _dbContext.Users.Where(u => u.MyBars.Any(b => b.Id == Bar.Id));
            if (users != null)
            {
                foreach (var user in users)
                {
                    var barToDelete = user.MyBars.FirstOrDefault(b => b.Id == Bar.Id);
                    if (barToDelete != null)
                    {
                        user.MyBars.Remove(barToDelete);
                    }
                }
            }
            return await _barRepository.DeleteBar(Bar);
        }
    }

}
