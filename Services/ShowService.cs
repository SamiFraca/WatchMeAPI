using WatchMe.Models;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WatchMe.Services
{
    public class ShowsService
    {
        private readonly DataContext _dbContext;

        public ShowsService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Show>> GetShows()
        {
            if (_dbContext.Shows == null)
            {
                throw new Exception("Shows not found");
            }
            return await _dbContext.Shows.ToListAsync();
        }

        public async Task<Show> GetShow(int id)
        {
            if (_dbContext.Shows == null)
            {
                throw new Exception("Shows not found");
            }
            var Show = await _dbContext.Shows.FindAsync(id);
            if (Show == null)
            {
                throw new Exception("Show not found");
            }
            return Show;
        }

        public async Task<Show> PostShow(Show show)
        {
            if (show.BarId != 0 || show.BarId != null)
            {
                _dbContext.Shows.Add(show);
                await _dbContext.SaveChangesAsync();
                return show;
            }
            throw new Exception("Invalid show data");
        }

        public async Task<bool> PutShow(int id, Show show)
        {
            if (id != show.Id)
            {
                throw new Exception("Invalid show id");
            }
            _dbContext.Entry(show).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
                {
                    throw new Exception("Show not found");
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public bool ShowExists(long id)
        {
            return (_dbContext.Shows?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<bool> DeleteShow(int id)
        {
            if (_dbContext.Shows is null)
            {
                throw new Exception("Shows not found");
            }

            var Show = await _dbContext.Shows.FindAsync(id);
            if (Show == null)
            {
                throw new Exception("Show not found");
            }
            _dbContext.Shows.Remove(Show);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}