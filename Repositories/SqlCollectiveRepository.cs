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
        return await dbContext.CollectiveMembers
            .AsNoTracking()
            .Include(member => member.Games)
            .AsSplitQuery()
            .OrderBy(member => member.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesAsync()
    {
        return await dbContext.Games
            .AsNoTracking()
            .OrderBy(game => game.Title)
            .ThenBy(game => game.ReleaseYear)
            .ToListAsync();
    }

    public async Task AddGameAsync(Game game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds)
    {
        if (gameIds.Count > 0)
        {
            var selectedGames = await dbContext.Games
                .Where(game => gameIds.Contains(game.Id))
                .ToListAsync();

            member.Games = selectedGames;
        }

        dbContext.CollectiveMembers.Add(member);
        await dbContext.SaveChangesAsync();
    }

}
