using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;
using WatchMe.Services;
using WatchMe.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace WatchMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly ShowsService _showsService;
        private readonly BarService _barService;

        public ShowsController(ShowsService showService, BarService barService)
        {
            _showsService = showService;
            _barService = barService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> GetShows()
        {
            var shows = await _showsService.GetShows();
            return new OkObjectResult(shows);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Show>> GetShow([FromRoute] int id)
        {
            var show = await _showsService.GetShow(id);
            return Ok(show);
        }

        [HttpPost]
        public async Task<ActionResult<Show>> PostShow([FromBody] Show show)
        {
            var existingBar = await _barService.GetBarAsync(show.BarId);
            if (existingBar == null)
            {
                return NotFound("The specified bar does not exist");
            }
            var newShow = await _showsService.PostShow(show);
            return CreatedAtAction(nameof(GetShow), new { id = newShow.Id }, newShow);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Show>> PutShow([FromRoute] int id, [FromBody] Show show)
        {
            if (id != show.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _showsService.PutShow(id, show);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_showsService.ShowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteShow(int id)
        {
            var result = await _showsService.DeleteShow(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBarAsync(
            [FromBody] JsonPatchDocument<Show> show,
            [FromRoute] int id
        )
        {
            var updatedBar = await _showsService.UpdateBarPatchAsync(id, show);
            if (updatedBar == null)
            {
                return NotFound();
            }
            return Ok(updatedBar);
        }
    }
}
