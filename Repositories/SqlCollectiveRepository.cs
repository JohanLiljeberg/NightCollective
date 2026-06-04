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
        return dbContext.CollectiveMembers
            .AsNoTracking()
            .Include(member => member.Games)
            .OrderBy(member => member.Id)
            .ToList();
    }
}
