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
        //   public async Task<ActionResult<Bar>> LoginVerify(string name, string password){
        //     var Bar = await _dbContext.Bars.SingleOrDefaultAsync(
        //             e => e.Barname == name && e.Password == password
        //         );
        //         return Bar;
        //   }
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