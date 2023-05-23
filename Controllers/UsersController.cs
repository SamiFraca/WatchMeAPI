using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WatchMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
namespace WatchMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //  public static List<User> allUser = User.GetUsersFromJSON();
        private readonly UserService _userService;

        // private readonly ILogger<UsersController> _logger;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            // _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            if (users == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(users);
        }

        //  [HttpGet("{id}")]
        //         public async Task<ActionResult<User>> GetUser(int id)
        //         {
        //             var user = await _userService.GetUser(id);
        //             if (user == null)
        //             {
        //                 return new NotFoundObjectResult(null);
        //             }
        //             return new OkObjectResult(user);
        //             var User = await _dbContext.Users.FindAsync(id);
        //             if (User == null)
        //             {
        //                 return NotFound();
        //             }
        //        }
        [EnableCors("AllowAllOrigins")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(
            int userId,
            [FromHeader(Name = "Authorization")] string authorization
        )
        {
            try
            {
                var tokenValue = authorization.Split(" ")[1];
                var user = await _userService.GetUser(userId, tokenValue);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            //   var user = await _userService.GetUser(id, token);
            // if (user == null)
            // {
            //     return new NotFoundObjectResult(null);
            // }
            // return new OkObjectResult(user);
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult<User>> LoginVerify(string name, string password)
        {
            var authService = HttpContext.RequestServices.GetService<AuthService>();
            return await _userService.LoginVerify(name, password, authService, HttpContext);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            return await _userService.PostUser(User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            return await _userService.PutUser(id, User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}
