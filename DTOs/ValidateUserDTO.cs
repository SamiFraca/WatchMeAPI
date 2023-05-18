using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Models;

namespace WatchMe.DTOs
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserDTO : ControllerBase
    {
        private readonly DataContext _dbContext;

        public UserDTO(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDto GetUserDTO(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            if (user.Id == id)
            {
                var userDto = new UserDto
                {
                    Name = user.Username,
                    IsAdmin = user.IsAdmin
                };

                return userDto;
            }

            return null;
        }
    }

    public class UserDto
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
