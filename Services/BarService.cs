using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace WatchMe.Services
{
    public class BarService
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<BarService> _logger;

        public BarService(DataContext dbContext, ILogger<BarService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Bar>> GetBarsAsync()
        {
            return await _dbContext.Bars.Include(bar => bar.Shows).ToListAsync();
        }

        public async Task<Bar> GetBarAsync(int id)
        {
            return await _dbContext.Bars
                .Include(bar => bar.Shows)
                .FirstOrDefaultAsync(bar => bar.Id == id);
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

        public async Task<IActionResult> PostBarAsync(Bar bar)
        {
            _dbContext.Bars.Add(bar);
            await _dbContext.SaveChangesAsync();
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
            _dbContext.Bars.Remove(Bar);
            await _dbContext.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
