using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WatchMe.Repositories;
using Microsoft.AspNetCore.Http;

namespace WatchMe.Services
{
    public class UserService
    {private readonly UserRepository _userRepository;
        private readonly DataContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(DataContext dbContext, ILogger<UserService> logger, UserRepository userRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            return users;
        }

        public async Task<User> GetUser(int id)
        {
              var user = await _userRepository.GetUser(id);

            return user;
        }

public async Task<ActionResult<User>> LoginVerify(string name, string password, AuthService authService, HttpContext httpContext)
{
    try
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(
            e => e.Username == name && e.Password == password
        );
        if (user == null)
        {
            return new NotFoundObjectResult("Invalid username or password");
        }
        var LoggedUser = user;
        var token = authService.AuthenticateUser(LoggedUser);
        if (token == null)
        {
            return new UnauthorizedObjectResult("Invalid username or password");
        }

       // Send token through HttpOnly cookie
        httpContext.Response.Cookies.Append("Authorization", token, new CookieOptions
         {
            HttpOnly = true
         });

        return new OkObjectResult(new { Token = token });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while verifying the login details.");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}

        public async Task<User> PostUser(User User)
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
            var user = await _userRepository.PostUser(User);
            return user;
        }

        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.Id)
            {
                return new BadRequestObjectResult(null);
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
                    return new NotFoundObjectResult($"No user found with '{User.Username}' name.");
                }
                else
                {
                    throw;
                }
            }
            return new NoContentResult();
        }

        private bool UserExists(long id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _userRepository.GetUser(id);
            if (User == null)
            {
                return new NotFoundObjectResult(null);
            }
         var result = await _userRepository.DeleteUser(User);
         return result;
        }
    }
}
