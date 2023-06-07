using WatchMe.Models;
using Microsoft.AspNetCore.JsonPatch;
using WatchMe.Data;
using Microsoft.EntityFrameworkCore;
public interface IBarPatcher
{
    Task<Bar> UpdateBarPatchAsync(int id, JsonPatchDocument<Bar> bar);
}

public class BarPatcher : IBarPatcher
{
    private readonly DataContext _dbContext;

    public BarPatcher(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Bar> UpdateBarPatchAsync(int id, JsonPatchDocument<Bar> bar)
    {
        var barQuery = await _dbContext.Bars.FirstOrDefaultAsync(bar => bar.Id == id);
        if (barQuery == null)
        {
            return barQuery;
        }
        bar.ApplyTo(barQuery);
        await _dbContext.SaveChangesAsync();

        return barQuery;
    }
}
