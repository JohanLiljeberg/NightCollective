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

    public async Task<CollectiveMember?> GetCollectiveMemberByIdAsync(int id)
    {
        return await dbContext.CollectiveMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(member => member.Id == id);
    }

    public async Task AddCollectiveMemberAsync(CollectiveMember member)
    {
        dbContext.CollectiveMembers.Add(member);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCollectiveMemberAsync(CollectiveMember member)
    {
        dbContext.CollectiveMembers.Update(member);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesAsync()
    {
        return await dbContext.Games
            .AsNoTracking()
            .Include(game => game.CollectiveMember)
            .OrderBy(game => game.Title)
            .ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await dbContext.Games
            .AsNoTracking()
            .Include(game => game.CollectiveMember)
            .FirstOrDefaultAsync(game => game.Id == id);
    }

    public async Task AddGameAsync(Game game, int collectiveMemberId)
    {
        game.CollectiveMemberId = collectiveMemberId;
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateGameAsync(Game game, int collectiveMemberId)
    {
        game.CollectiveMemberId = collectiveMemberId;
        dbContext.Games.Update(game);
        await dbContext.SaveChangesAsync();
    }
}
