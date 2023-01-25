using WatchMe.Models;
using WatchMe.Services;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;

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
        [HttpPost]
        public Task<IActionResult<User>> PostUser(User User)
        {
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
                await db_Context.SaveChangeAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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
            if (User is null)
            {
                return NotFound();
            }

            var User = await _dbContext.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(User);
            await _dbContext.SaveChangeAsync();
            return NoContent();
        }
    }
}