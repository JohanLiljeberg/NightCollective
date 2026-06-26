using Night.Models;

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
            Quote = "We champion small teams, expressive play, accessible tools, and games that belong in galleries as much as living rooms."
        }
    ];

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

    public Task<CollectiveMember?> GetCollectiveMemberByIdAsync(int id) =>
        Task.FromResult(CollectiveMembers.FirstOrDefault(member => member.Id == id));

    public Task AddCollectiveMemberAsync(CollectiveMember member)
    {
        member.Id = CollectiveMembers.Count == 0 ? 1 : CollectiveMembers.Max(item => item.Id) + 1;
        CollectiveMembers.Add(member);
        return Task.CompletedTask;
    }

    public Task UpdateCollectiveMemberAsync(CollectiveMember member)
    {
        var index = CollectiveMembers.FindIndex(item => item.Id == member.Id);
        if (index >= 0)
        {
            CollectiveMembers[index] = member;
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Game>> GetGamesAsync() =>
        Task.FromResult<IReadOnlyCollection<Game>>(CollectiveMembers.SelectMany(member => member.Games).ToList());

    public Task<Game?> GetGameByIdAsync(int id) =>
        Task.FromResult(CollectiveMembers.SelectMany(member => member.Games).FirstOrDefault(game => game.Id == id));

    public Task AddGameAsync(Game game, int collectiveMemberId)
    {
        var member = CollectiveMembers.FirstOrDefault(item => item.Id == collectiveMemberId);
        if (member is null) return Task.CompletedTask;

        game.Id = game.Id == 0 ? CollectiveMembers.SelectMany(item => item.Games).DefaultIfEmpty().Max(item => item?.Id ?? 0) + 1 : game.Id;
        game.CollectiveMemberId = collectiveMemberId;
        game.CollectiveMember = member;
        member.Games.Add(game);
        return Task.CompletedTask;
    }

    public Task UpdateGameAsync(Game game, int collectiveMemberId)
    {
        foreach (var member in CollectiveMembers)
        {
            member.Games.RemoveAll(item => item.Id == game.Id);
        }

        return AddGameAsync(game, collectiveMemberId);
    }
}
