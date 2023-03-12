using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace WatchMe.Controllers
{
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    [Route("[controller]")]
    public class BarsController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<BarsController> _logger;

        public BarsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bar>>> GetBars()
        {
            if (_dbContext.Bars == null)
            {
                return NotFound();
            }
            var bars = _dbContext.Bars
                .Include(bar => bar.Shows)
                //  .Where(bar => bar.)
                .ToListAsync();
            return await bars;
            //    return await bars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            if (_dbContext.Bars == null)
            {
                return NotFound();
            }
            var Bar = await _dbContext.Bars.FindAsync(id);
            if (Bar == null)
            {
                return NotFound();
            }
            return Bar;
        }

        [HttpGet("locations")]
        public IActionResult SearchLocation(string location)
        {
            if (_dbContext.Bars == null)
            {
                return NotFound();
            }
            var bars = _dbContext.Bars
                .Where(b => b.Location.Contains(location))
                .Include(b => b.Shows)
                .ToList();

            if (bars.Count == 0)
            {
                return NotFound($"No bars found with location '{location}'");
            }
            return Ok(bars);
        }
        [HttpGet("name")]
        public IActionResult SearchName(string name)
        {
            if (_dbContext.Bars == null)
            {
                return NotFound();
            }
            var bars = _dbContext.Bars
                .Where(b => b.Name.Contains(name))
                .Include(b => b.Shows)
                .ToList();

            if (bars.Count == 0)
            {
                return NotFound($"No bars found with name '{name}'");
            }
            return Ok(bars);
        }

        [HttpPost]
        public async Task<ActionResult<Bar>> PostBar(Bar Bar)
        {
            _dbContext.Bars.Add(Bar);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBar), new { id = Bar.Id }, Bar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBar(int id, Bar Bar)
        {
            if (id != Bar.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(Bar).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool BarExists(long id)
        {
            return (_dbContext.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBar(int id)
        {
            if (_dbContext.Bars is null)
            {
                return NotFound();
            }

            var Bar = await _dbContext.Bars.FindAsync(id);
            if (Bar == null)
            {
                return NotFound();
            }
            _dbContext.Bars.Remove(Bar);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
