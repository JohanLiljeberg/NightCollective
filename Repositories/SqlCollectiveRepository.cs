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
            .Include(game => game.Members)
            .AsSplitQuery()
            .OrderBy(game => game.Title)
            .ThenBy(game => game.ReleaseYear)
            .ToListAsync();
    }

    public async Task AddGameAsync(Game game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateGameAsync(Game game)
    {
        var existingGame = await dbContext.Games.FirstOrDefaultAsync(item => item.Id == game.Id);

        if (existingGame is null)
        {
            return;
        }

        existingGame.Title = game.Title;
        existingGame.Image = game.Image;
        existingGame.ReleaseYear = game.ReleaseYear;
        existingGame.DeveloperPublisher = game.DeveloperPublisher;
        existingGame.Platforms = game.Platforms;
        existingGame.GenreGameplayType = game.GenreGameplayType;
        existingGame.FromCollective = game.FromCollective;

        await dbContext.SaveChangesAsync();
    }

    public async Task AddCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds)
    {
        member.Games = await GetSelectedGamesAsync(gameIds);

        dbContext.CollectiveMembers.Add(member);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds)
    {
        var existingMember = await dbContext.CollectiveMembers
            .Include(item => item.Games)
            .FirstOrDefaultAsync(item => item.Id == member.Id);

        if (existingMember is null)
        {
            return;
        }

        existingMember.Name = member.Name;
        existingMember.Image = member.Image;
        existingMember.Position = member.Position;
        existingMember.Quote = member.Quote;
        existingMember.Games.Clear();
        existingMember.Games.AddRange(await GetSelectedGamesAsync(gameIds));

        await dbContext.SaveChangesAsync();
    }

    private async Task<List<Game>> GetSelectedGamesAsync(IReadOnlyCollection<int> gameIds)
    {
        if (gameIds.Count == 0)
        {
            return [];
        }

        return await dbContext.Games
            .Where(game => gameIds.Contains(game.Id))
            .ToListAsync();
    }
}
