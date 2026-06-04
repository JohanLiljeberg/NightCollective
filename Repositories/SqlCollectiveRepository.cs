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

    public IReadOnlyCollection<Developer> GetMembers()
    {
        return dbContext.Developers
            .AsNoTracking()
            .OrderBy(developer => developer.Id)
            .ToList();
    }
}
