using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WatchMe.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<ShowsController> _logger;
        public ShowsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Show>>> GetShows()
        {
            if (_dbContext.Shows == null)
            {
                return NotFound();
            }
            return await _dbContext.Shows.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Show>> GetShow(int id)
        {
            if (_dbContext.Shows == null)
            {
                return NotFound();
            }
            var Show = await _dbContext.Shows.FindAsync(id);
            if (Show == null)
            {
                return NotFound();
            }
            return Show;
        }
        [HttpPost]
        public async Task<ActionResult<Show>> PostShow(Show Show)
        {
            _dbContext.Shows.Add(Show);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetShow), new { id = Show.Id }, Show);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShow(int id, Show Show)
        {
            if (id != Show.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(Show).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
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

        private bool ShowExists(long id)
        {
            return (_dbContext.Shows?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            if (_dbContext.Shows is null)
            {
                return NotFound();
            }

            var Show = await _dbContext.Shows.FindAsync(id);
            if (Show == null)
            {
                return NotFound();
            }
            _dbContext.Shows.Remove(Show);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}