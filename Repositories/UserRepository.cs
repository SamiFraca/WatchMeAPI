using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
namespace WatchMe.Repositories
{
    interface IUserRepository
    {
        public List<User> GetUsers();
    }
    public class UserRepository 
    {
          private readonly DataContext _dbContext;
          public UserRepository(DataContext dbContext){
            _dbContext = dbContext;
          }

          public async Task<List<User>> GetUsers(){
                        var users = await _dbContext.Users.Include(user => user.MyBar).ToListAsync();
                        return users;
          }
          public async Task<User> GetUser(int id ){
            var user = await _dbContext.Users.FindAsync(id);
            return user; 
          }
        //   public async Task<ActionResult<User>> LoginVerify(string name, string password){
        //     var user = await _dbContext.Users.SingleOrDefaultAsync(
        //             e => e.Username == name && e.Password == password
        //         );
        //         return user;
        //   }
         public async Task<User> PostUser(User user){
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
         }
        public async Task<IActionResult> DeleteUser(User user){
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return new ContentResult();
        }
    }
}