using WatchMe.Models;
using WatchMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace WatchMe.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //  public static List<User> allUser = User.GetUsersFromJSON();
        private readonly DataContext _dbContext;
        private readonly ILogger<UsersController> _logger;
        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = UserService.Get(id);

            if (User == null)
                return NotFound();

            return await User;
        }
        [HttpPost]
        public IActionResult Create(User User)
        {
            UserService.Add(User);
            return CreatedAtAction(nameof(Get), new { id = User.Id }, User);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User User)
        {
            if (id != User.Id)
                return BadRequest();

            var existingUser = UserService.Get(id);
            if (existingUser is null)
                return NotFound();

            UserService.Update(User);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var User = UserService.Get(id);

            if (User is null)
                return NotFound();

            UserService.Delete(id);

            return NoContent();
        }
    }
}