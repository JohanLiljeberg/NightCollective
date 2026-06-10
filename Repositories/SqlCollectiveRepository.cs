using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Models;

namespace Night.Repositories;

public class SqlCollectiveRepository(AppDbContext dbContext) : ICollectiveRepository
{
    public async Task<IReadOnlyCollection<CollectiveProject>> GetFeaturedProjectsAsync()
    {
        return await dbContext.CollectiveProjects
            .AsNoTracking()
            .OrderBy(project => project.Id)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingEventsAsync(DateTime fromDate)
    {
        return await dbContext.CollectiveEvents
            .AsNoTracking()
            .Where(collectiveEvent => collectiveEvent.Date >= fromDate.Date)
            .OrderBy(collectiveEvent => collectiveEvent.Date)
            .ThenBy(collectiveEvent => collectiveEvent.Title)
            .ToListAsync();
    }

    public async Task<CollectiveEvent?> GetNextUpcomingEventAsync(DateTime fromDate)
    {
        return await dbContext.CollectiveEvents
            .AsNoTracking()
            .Where(collectiveEvent => collectiveEvent.Date >= fromDate.Date)
            .OrderBy(collectiveEvent => collectiveEvent.Date)
            .ThenBy(collectiveEvent => collectiveEvent.Title)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync()
    {
        int pageSize = 10;
        int pageNumber = 0;

        return await dbContext.CollectiveMembers
            .AsNoTracking()
            .Include(member => member.Games)
            .AsSplitQuery()
            .OrderBy(member => member.Id)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
