using WatchMe.Models;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WatchMe.Repositories;
using Microsoft.AspNetCore.JsonPatch;
namespace WatchMe.Services
{
    public class ShowsService
    {
        private readonly ShowsRepository _showRepository;
        private readonly DataContext _dbContext;

        public ShowsService(ShowsRepository showRepository, DataContext dbContext)
        {
            _showRepository = showRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Show>> GetShows()
        {
            List<Show> shows = await _showRepository.GetShows();
            if (shows == null)
            {
                throw new Exception("Shows not found");
            }
            return shows;
        }

        public async Task<Show> GetShow(int id)
        {
            var show = await _showRepository.GetShow(id);
            if (show == null)
            {
                throw new Exception("Show not found");
            }
            return show;
        }

        public async Task<Show> PostShow(Show show)
        {
            if (show.BarId != 0 || show.BarId != null)
            {
                return await _showRepository.PostShow(show);
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
            var Show = await _showRepository.GetShow(id);
            if (Show == null)
            {
                throw new Exception("Show not found");
            }
            return await _showRepository.DeleteShow(Show);
        }

        public async Task<Show> UpdateBarPatchAsync(int id, JsonPatchDocument<Show> show)
        {
            var showQuery = await _dbContext.Shows.FirstOrDefaultAsync(show => show.Id == id);
            if (showQuery == null)
            {
                return showQuery;
            }
            show.ApplyTo(showQuery);
            await _dbContext.SaveChangesAsync();

            return showQuery;
        }
    }
}
