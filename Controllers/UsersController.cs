using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            var users = _dbContext.Users
                .Include(user => user.MyBar)
                //  .Where(bar => bar.)
                .ToListAsync();
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var User = await _dbContext.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return User;
        }

        // if (_dbContext.Users == null)
        // {
        //     return NotFound();
        // }
        // var user = await _dbContext.Users.SingleOrDefaultAsync(
        //     user => user.Username == name && user.Password == password
        // );
        // if (user == null)
        // {
        //     return NotFound();
        // }
        // // var token = await GetToken(user.Username, user.Role);
        // return user;
        [HttpPost("auth/login")]
        public async Task<ActionResult<User>> LoginVerify(string name, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(
                e => e.Username == name && e.Password == password
            );
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var LoggedUser =  user;
            var authService = new AuthService(AuthService.KeyGen());
            var token = authService.AuthenticateUser(LoggedUser);
            if (token == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(new { Token = token });
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            if (User.MyBar == null)
            {
                User.MyBar = new Bar
                {
                    Id = 0,
                    Name = "Unknown",
                    Location = "None",
                    Capacity = 0,
                    Shows = null
                };
            }
            _dbContext.Users.Add(User);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(User).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        private bool UserExists(long id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_dbContext.Users is null)
            {
                return NotFound();
            }

            var User = await _dbContext.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(User);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
