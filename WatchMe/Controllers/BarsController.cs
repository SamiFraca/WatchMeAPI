using WatchMe.Models;
using WatchMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace WatchMe.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BarsController : ControllerBase
    {
        //  public static List<Bar> allbar = Bar.GetUsersFromJSON();

        private readonly ILogger<BarsController> _logger;
        public BarsController()
        {
        }

        [HttpGet]
        public ActionResult<List<Bar>> GetAll() =>
         BarService.GetAll();
        [HttpGet("{id}")]
        public ActionResult<Bar> Get(int id)
        {
            var Bar = BarService.Get(id);

            if (Bar == null)
                return NotFound();

            return Bar;
        }
        [HttpPost]
        public IActionResult Create(Bar Bar)
        {
            BarService.Add(Bar);
            return CreatedAtAction(nameof(Get), new { id = Bar.Id }, Bar);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Bar Bar)
        {
            if (id != Bar.Id)
                return BadRequest();

            var existingBar = BarService.Get(id);
            if (existingBar is null)
                return NotFound();

            BarService.Update(Bar);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Bar = BarService.Get(id);

            if (Bar is null)
                return NotFound();

            BarService.Delete(id);

            return NoContent();
        }
    }
}