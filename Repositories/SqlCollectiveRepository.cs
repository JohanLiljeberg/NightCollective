using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Models;
using Night.ViewModels;

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
            .Include(member => member.FeaturedGame)
            .Include(member => member.GameContributions)
                .ThenInclude(gc => gc.Game)
            .AsSplitQuery()
            .OrderBy(member => member.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesAsync()
    {
        return await dbContext.Games
            .AsNoTracking()
            .Include(game => game.Screenshots)
            .Include(game => game.MemberContributions)
                .ThenInclude(mc => mc.CollectiveMember)
            .AsSplitQuery()
            .OrderBy(game => game.Title)
            .ThenBy(game => game.ReleaseYear)
            .ToListAsync();
    }

    public async Task AddGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();

        if (contributions.Any())
        {
            foreach (var contribution in contributions)
            {
                var gameMemberContribution = new GameMemberContribution
                {
                    GameId = game.Id,
                    CollectiveMemberId = contribution.MemberId,
                    InvolvementLevel = contribution.InvolvementLevel,
                    WorkAreas = contribution.SelectedWorkAreas
                };

                dbContext.Set<GameMemberContribution>().Add(gameMemberContribution);
            }

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions)
    {
        var existingGame = await dbContext.Games
            .Include(g => g.MemberContributions)
            .Include(g => g.Screenshots)
            .FirstOrDefaultAsync(item => item.Id == game.Id);

        if (existingGame is null)
        {
            return;
        }

        existingGame.Title = game.Title;
        existingGame.Image = game.Image;
        existingGame.ImageSmallUrl = game.ImageSmallUrl;
        existingGame.ImageMediumUrl = game.ImageMediumUrl;
        existingGame.ImageLargeUrl = game.ImageLargeUrl;
        existingGame.ReleaseYear = game.ReleaseYear;
        existingGame.DeveloperPublisher = game.DeveloperPublisher;
        existingGame.Platforms = game.Platforms;
        existingGame.GenreGameplayType = game.GenreGameplayType;
        existingGame.FromCollective = game.FromCollective;
        existingGame.Description = game.Description;
        existingGame.YouTubeTrailerUrl = game.YouTubeTrailerUrl;

        // Update screenshots - remove existing and add new ones
        if (existingGame.Screenshots.Any())
        {
            dbContext.GameScreenshots.RemoveRange(existingGame.Screenshots);
        }

        if (game.Screenshots.Any())
        {
            foreach (var screenshot in game.Screenshots)
            {
                screenshot.GameId = existingGame.Id;
                dbContext.GameScreenshots.Add(screenshot);
            }
        }

        // Remove existing contributions
        dbContext.Set<GameMemberContribution>().RemoveRange(existingGame.MemberContributions);

        // Add updated contributions
        if (contributions.Any())
        {
            foreach (var contribution in contributions)
            {
                var gameMemberContribution = new GameMemberContribution
                {
                    GameId = game.Id,
                    CollectiveMemberId = contribution.MemberId,
                    InvolvementLevel = contribution.InvolvementLevel,
                    WorkAreas = contribution.SelectedWorkAreas
                };

                dbContext.Set<GameMemberContribution>().Add(gameMemberContribution);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (game is null)
        {
            return;
        }

        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds, IReadOnlyCollection<MemberGameContributionFormViewModel> gameContributions)
    {
        member.Games = await GetSelectedGamesAsync(gameIds);

        dbContext.CollectiveMembers.Add(member);
        await dbContext.SaveChangesAsync();

        if (gameContributions.Any())
        {
            foreach (var contribution in gameContributions)
            {
                var gameMemberContribution = new GameMemberContribution
                {
                    GameId = contribution.GameId,
                    CollectiveMemberId = member.Id,
                    InvolvementLevel = contribution.InvolvementLevel,
                    WorkAreas = contribution.SelectedWorkAreas
                };

                dbContext.Set<GameMemberContribution>().Add(gameMemberContribution);
            }

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds, IReadOnlyCollection<MemberGameContributionFormViewModel> gameContributions)
    {
        var existingMember = await dbContext.CollectiveMembers
            .Include(item => item.Games)
            .Include(item => item.GameContributions)
            .FirstOrDefaultAsync(item => item.Id == member.Id);

        if (existingMember is null)
        {
            return;
        }

        existingMember.Name = member.Name;
        existingMember.Image = member.Image;
        existingMember.Position = member.Position;
        existingMember.Quote = member.Quote;
        existingMember.MembershipType = member.MembershipType;
        existingMember.FeaturedGameId = member.FeaturedGameId;
        existingMember.Games.Clear();
        existingMember.Games.AddRange(await GetSelectedGamesAsync(gameIds));

        // Remove existing game contributions
        dbContext.Set<GameMemberContribution>().RemoveRange(existingMember.GameContributions);

        // Add updated contributions
        if (gameContributions.Any())
        {
            foreach (var contribution in gameContributions)
            {
                var gameMemberContribution = new GameMemberContribution
                {
                    GameId = contribution.GameId,
                    CollectiveMemberId = member.Id,
                    InvolvementLevel = contribution.InvolvementLevel,
                    WorkAreas = contribution.SelectedWorkAreas
                };

                dbContext.Set<GameMemberContribution>().Add(gameMemberContribution);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCollectiveMemberAsync(int id)
    {
        var member = await dbContext.CollectiveMembers.FindAsync(id);
        if (member is not null)
        {
            dbContext.CollectiveMembers.Remove(member);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<SiteDisplaySettings> GetDisplaySettingsAsync()
    {
        var settings = await dbContext.SiteDisplaySettings.AsNoTracking().FirstOrDefaultAsync();

        return settings ?? new SiteDisplaySettings { Id = 1 };
    }

    public async Task UpdateDisplaySettingsAsync(SiteDisplaySettings settings)
    {
        var existing = await dbContext.SiteDisplaySettings.FirstOrDefaultAsync();

        if (existing is null)
        {
            settings.Id = 1;
            dbContext.SiteDisplaySettings.Add(settings);
        }
        else
        {
            existing.ShowFullMembers = settings.ShowFullMembers;
            existing.ShowSubscribedMembers = settings.ShowSubscribedMembers;
            existing.ShowUnsubscribedMembers = settings.ShowUnsubscribedMembers;
            existing.ShowCollectiveGames = settings.ShowCollectiveGames;
            existing.ShowExternalGames = settings.ShowExternalGames;
        }

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

    private async Task<List<CollectiveMember>> GetSelectedMembersAsync(IReadOnlyCollection<int> memberIds)
    {
        if (memberIds.Count == 0)
        {
            return [];
        }

        return await dbContext.CollectiveMembers
            .Where(member => memberIds.Contains(member.Id))
            .ToListAsync();
    }
}
