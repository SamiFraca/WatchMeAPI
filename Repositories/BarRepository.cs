using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
namespace WatchMe.Repositories
{
    interface IBarRepository
    {
        public List<Bar> GetBars();
    }
    public class BarRepository 
    {
          private readonly DataContext _dbContext;
          public BarRepository(DataContext dbContext){
            _dbContext = dbContext;
          }

          public async Task<List<Bar>> GetBars(){
                        var Bars = await _dbContext.Bars.Include(Bar => Bar.Shows).ToListAsync();
                        return Bars;
          }
          
          public async Task<Bar> GetBar(int id ){
                return await _dbContext.Bars
                .Include(bar => bar.Shows)
                .FirstOrDefaultAsync(bar => bar.Id == id);
          }
           public async Task<List<Bar>> GetBarUser(int id ){
            var UserBars =await _dbContext.Bars
                .Where(bar => bar.UserId == id).Include(bar => bar.Shows).ToListAsync();
                return UserBars;
          }
         public async Task<Bar> PostBar(Bar Bar){
            _dbContext.Bars.Add(Bar);
            await _dbContext.SaveChangesAsync();
            return Bar;
         }
        public async Task<IActionResult> DeleteBar(Bar Bar){
            _dbContext.Bars.Remove(Bar);
            await _dbContext.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}