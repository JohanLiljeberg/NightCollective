using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Models;

namespace Night.Repositories;

public class SqlCollectiveRepository(AppDbContext dbContext) : ICollectiveRepository
{
    public IReadOnlyCollection<CollectiveProject> GetFeaturedProjects()
    {
        return dbContext.CollectiveProjects
            .AsNoTracking()
            .OrderBy(project => project.Id)
            .ToList();
    }

    public IReadOnlyCollection<CollectiveEvent> GetUpcomingEvents()
    {
        return dbContext.CollectiveEvents
            .AsNoTracking()
            .OrderBy(collectiveEvent => collectiveEvent.Id)
            .ToList();
    }

    public IReadOnlyCollection<CollectiveMember> GetCollectiveMembers()
    {
        int pageSize = 10;
        int pageNumber = 0; 

        return dbContext.CollectiveMembers
            .AsNoTracking()
            .Include(member => member.Games)
            .AsSplitQuery()
            .OrderBy(member => member.Id)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
