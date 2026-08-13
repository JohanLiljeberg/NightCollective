using Night.Models;
using Night.ViewModels;

namespace Night.Repositories;

public class InMemoryCollectiveRepository : ICollectiveRepository
{
    private static readonly IReadOnlyCollection<CollectiveProject> FeaturedProjects =
    [
        new CollectiveProject
        {
            Title = "Dream Cartographers",
            Creator = "Mira Sol",
            Medium = "Interactive poem",
            Description = "A tiny exploration game about mapping memories, procedural stars, and unfinished conversations."
        },
        new CollectiveProject
        {
            Title = "Arcade Reliquary",
            Creator = "Night Collective Studio",
            Medium = "Playable installation",
            Description = "A cabinet-scale exhibition that treats high scores, rituals, and glitches as community folklore."
        },
        new CollectiveProject
        {
            Title = "Soft Boss Rush",
            Creator = "Jun Vale",
            Medium = "Experimental action game",
            Description = "A non-violent boss rush where every encounter is resolved through rhythm, dialogue, and care."
        }
    ];

    private static readonly IReadOnlyCollection<CollectiveEvent> UpcomingEvents =
    [
        new CollectiveEvent
        {
            Id = 1,
            Title = "Monthly Play Salon",
            Date = new DateTime(2026, 7, 3),
            Location = "Online + local pop-up",
            Description = "A gentle critique circle for prototypes, visual experiments, and strange playable ideas.",
            ImageSmallUrl = "/images/events/monthly-gamejam/monthly-gamejam_sm.webp",
            ImageMediumUrl = "/images/events/monthly-gamejam/monthly-gamejam_md.webp",
            ImageLargeUrl = "/images/events/monthly-gamejam/monthly-gamejam_lg.webp"
        },
        new CollectiveEvent
        {
            Id = 2,
            Title = "Games as Art Showcase",
            Date = new DateTime(2026, 8, 14),
            Location = "Community gallery",
            Description = "A curated evening celebrating independent game creation, installations, talks, and live demos.",
            ImageSmallUrl = "/images/events/games-as-art-showcase/games-as-art-showcase_sm.webp",
            ImageMediumUrl = "/images/events/games-as-art-showcase/games-as-art-showcase_md.webp",
            ImageLargeUrl = "/images/events/games-as-art-showcase/games-as-art-showcase_lg.webp"
        }
    ];

    private static readonly List<CollectiveMember> CollectiveMembers =
    [
        new CollectiveMember
        {
            Id = 1,
            Name = "Night Collective",
            Image = "/images/collective-members/night-collective.jpg",
            Position = "Curators, developers, artists, and players",
            Quote = "We champion small teams, expressive play, accessible tools, and games that belong in galleries as much as living rooms.",
            MembershipType = MembershipType.Full
        }
    ];

    private static readonly List<Game> Games = [];

    public Task<IReadOnlyCollection<CollectiveProject>> GetFeaturedProjectsAsync() => Task.FromResult(FeaturedProjects);

    public Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingEventsAsync(DateTime fromDate)
    {
        var events = UpcomingEvents
            .Where(collectiveEvent => collectiveEvent.Date >= fromDate.Date)
            .OrderBy(collectiveEvent => collectiveEvent.Date)
            .ToList();

        return Task.FromResult<IReadOnlyCollection<CollectiveEvent>>(events);
    }

    public Task<CollectiveEvent?> GetNextUpcomingEventAsync(DateTime fromDate)
    {
        var collectiveEvent = UpcomingEvents
            .Where(item => item.Date >= fromDate.Date)
            .OrderBy(item => item.Date)
            .FirstOrDefault();

        return Task.FromResult(collectiveEvent);
    }

    public Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync() => Task.FromResult<IReadOnlyCollection<CollectiveMember>>(CollectiveMembers);

    public Task<IReadOnlyCollection<Game>> GetGamesAsync() => Task.FromResult<IReadOnlyCollection<Game>>(Games);

    public Task AddGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions)
    {
        game.Id = Games.Count == 0 ? 1 : Games.Max(item => item.Id) + 1;

        // For in-memory, just add the game with members from contributions
        var memberIds = contributions.Select(c => c.MemberId).ToList();
        game.Members = CollectiveMembers.Where(member => memberIds.Contains(member.Id)).ToList();

        // Create contribution relationships
        foreach (var contribution in contributions)
        {
            var gameMemberContribution = new GameMemberContribution
            {
                GameId = game.Id,
                CollectiveMemberId = contribution.MemberId,
                Game = game,
                CollectiveMember = CollectiveMembers.First(m => m.Id == contribution.MemberId),
                InvolvementLevel = contribution.InvolvementLevel,
                WorkAreas = contribution.SelectedWorkAreas
            };

            game.MemberContributions.Add(gameMemberContribution);
        }

        Games.Add(game);

        return Task.CompletedTask;
    }

    public Task UpdateGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions)
    {
        var existingGame = Games.FirstOrDefault(item => item.Id == game.Id);

        if (existingGame is null)
        {
            return Task.CompletedTask;
        }

        existingGame.Title = game.Title;
        existingGame.Image = game.Image;
        existingGame.ReleaseYear = game.ReleaseYear;
        existingGame.DeveloperPublisher = game.DeveloperPublisher;
        existingGame.Platforms = game.Platforms;
        existingGame.GenreGameplayType = game.GenreGameplayType;
        existingGame.FromCollective = game.FromCollective;

        // Update contributions (simplified for in-memory)
        existingGame.MemberContributions.Clear();
        foreach (var contribution in contributions)
        {
            existingGame.MemberContributions.Add(new GameMemberContribution
            {
                GameId = game.Id,
                CollectiveMemberId = contribution.MemberId,
                InvolvementLevel = contribution.InvolvementLevel,
                WorkAreas = contribution.SelectedWorkAreas
            });
        }

        return Task.CompletedTask;
    }

    public Task DeleteGameAsync(int id)
    {
        var game = Games.FirstOrDefault(g => g.Id == id);

        if (game is not null)
        {
            Games.Remove(game);
        }

        return Task.CompletedTask;
    }

    public Task AddCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds, IReadOnlyCollection<MemberGameContributionFormViewModel> gameContributions)
    {
        member.Id = CollectiveMembers.Count == 0 ? 1 : CollectiveMembers.Max(item => item.Id) + 1;
        member.Games = Games.Where(game => gameIds.Contains(game.Id)).ToList();

        foreach (var contribution in gameContributions)
        {
            var game = Games.FirstOrDefault(g => g.Id == contribution.GameId);
            if (game is null)
            {
                continue;
            }

            member.GameContributions.Add(new GameMemberContribution
            {
                GameId = contribution.GameId,
                Game = game,
                CollectiveMemberId = member.Id,
                CollectiveMember = member,
                InvolvementLevel = contribution.InvolvementLevel,
                WorkAreas = contribution.SelectedWorkAreas
            });
        }

        CollectiveMembers.Add(member);

        return Task.CompletedTask;
    }

    public Task UpdateCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds, IReadOnlyCollection<MemberGameContributionFormViewModel> gameContributions)
    {
        var existingMember = CollectiveMembers.FirstOrDefault(item => item.Id == member.Id);

        if (existingMember is null)
        {
            return Task.CompletedTask;
        }

        existingMember.Name = member.Name;
        existingMember.Image = member.Image;
        existingMember.Position = member.Position;
        existingMember.Quote = member.Quote;
        existingMember.MembershipType = member.MembershipType;
        existingMember.FeaturedGameId = member.FeaturedGameId;
        existingMember.FeaturedGame = member.FeaturedGameId.HasValue 
            ? Games.FirstOrDefault(g => g.Id == member.FeaturedGameId.Value) 
            : null;
        existingMember.Games = Games.Where(game => gameIds.Contains(game.Id)).ToList();

        existingMember.GameContributions.Clear();
        foreach (var contribution in gameContributions)
        {
            var game = Games.FirstOrDefault(g => g.Id == contribution.GameId);
            if (game is null)
            {
                continue;
            }

            existingMember.GameContributions.Add(new GameMemberContribution
            {
                GameId = contribution.GameId,
                Game = game,
                CollectiveMemberId = existingMember.Id,
                CollectiveMember = existingMember,
                InvolvementLevel = contribution.InvolvementLevel,
                WorkAreas = contribution.SelectedWorkAreas
            });
        }

        return Task.CompletedTask;
    }

    public Task DeleteCollectiveMemberAsync(int id)
    {
        var member = CollectiveMembers.FirstOrDefault(m => m.Id == id);
        if (member is not null)
        {
            CollectiveMembers.Remove(member);
        }
        return Task.CompletedTask;
    }

    private static SiteDisplaySettings DisplaySettings { get; } = new() { Id = 1 };

    public Task<SiteDisplaySettings> GetDisplaySettingsAsync() => Task.FromResult(DisplaySettings);

    public Task UpdateDisplaySettingsAsync(SiteDisplaySettings settings)
    {
        DisplaySettings.ShowFullMembers = settings.ShowFullMembers;
        DisplaySettings.ShowSubscribedMembers = settings.ShowSubscribedMembers;
        DisplaySettings.ShowUnsubscribedMembers = settings.ShowUnsubscribedMembers;
        DisplaySettings.ShowCollectiveGames = settings.ShowCollectiveGames;
        DisplaySettings.ShowExternalGames = settings.ShowExternalGames;

        return Task.CompletedTask;
    }
}
