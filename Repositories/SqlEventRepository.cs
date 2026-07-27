using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Models;

namespace Night.Repositories;

public class SqlEventRepository(AppDbContext dbContext) : IEventRepository
{
    public async Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingAsync(DateTime fromDate)
    {
        return await dbContext.CollectiveEvents
            .AsNoTracking()
            .Where(collectiveEvent => collectiveEvent.Date >= fromDate.Date)
            .OrderBy(collectiveEvent => collectiveEvent.Date)
            .ThenBy(collectiveEvent => collectiveEvent.Title)
            .ToListAsync();
    }

    public async Task<CollectiveEvent?> GetByIdAsync(int id)
    {
        return await dbContext.CollectiveEvents
            .AsNoTracking()
            .FirstOrDefaultAsync(collectiveEvent => collectiveEvent.Id == id);
    }

    public async Task AddAsync(CollectiveEvent collectiveEvent)
    {
        await dbContext.CollectiveEvents.AddAsync(collectiveEvent);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(CollectiveEvent collectiveEvent)
    {
        dbContext.CollectiveEvents.Update(collectiveEvent);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var collectiveEvent = await dbContext.CollectiveEvents.FindAsync(id);
        if (collectiveEvent is null)
        {
            return;
        }

        dbContext.CollectiveEvents.Remove(collectiveEvent);
        await dbContext.SaveChangesAsync();
    }
}
