using WatchMe.Models;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WatchMe.Services
{
    public class ShowsRepository
    {
        private readonly DataContext _dbContext;


        public ShowsRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Show>> GetShows()
        {
            var shows = await _dbContext.Shows.ToListAsync();
            return shows;
        }

        public async Task<Show> GetShow(int id)
        {
            var Show = await _dbContext.Shows.FindAsync(id);
            return Show;
        }

        public async Task<Show> PostShow(Show show)
        {
                _dbContext.Shows.Add(show);
                await _dbContext.SaveChangesAsync();
                return show;
        }

        public async Task<bool> DeleteShow(Show show)
        {
         _dbContext.Shows.Remove(show);
            await _dbContext.SaveChangesAsync();
            return true;
    }}
}