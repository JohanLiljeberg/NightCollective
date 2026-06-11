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
            ImageUrl = "/images/events/monthly-gamejam.svg"
        },
        new CollectiveEvent
        {
            Id = 2,
            Title = "Games as Art Showcase",
            Date = new DateTime(2026, 8, 14),
            Location = "Community gallery",
            Description = "A curated evening celebrating independent game creation, installations, talks, and live demos.",
            ImageUrl = "/images/events/games-as-art-showcase.svg"
        }
    ];

    private static readonly IReadOnlyCollection<CollectiveMember> CollectiveMembers =
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

    public Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync() => Task.FromResult(CollectiveMembers);
}
