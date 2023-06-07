using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Models;
using WatchMe.Services;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace WatchMe.Controllers
{
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    [Route("[controller]")]
    public class BarPatchController : Controller
    {
        private readonly BarService _barService;
        private readonly DataContext _dbContext;
        private readonly IBarPatcher _barPatcher;

        public BarPatchController(
            BarService barService,
            DataContext dbContext,
            IBarPatcher barPatcher
        )
        {
            _barService = barService;
            _dbContext = dbContext;
            _barPatcher = barPatcher;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBarAsync(
            [FromBody] JsonPatchDocument<Bar>  bar,
            [FromRoute] int id
        )
        {
            var updatedBar = await _barPatcher.UpdateBarPatchAsync(id, bar);
            if (updatedBar == null)
            {
                return NotFound();
            }
            return Ok(updatedBar);
        }
    }
}
