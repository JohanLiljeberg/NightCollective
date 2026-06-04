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
            Title = "Monthly Play Salon",
            DateLabel = "First Friday",
            Location = "Online + local pop-up",
            Description = "A gentle critique circle for prototypes, visual experiments, and strange playable ideas."
        },
        new CollectiveEvent
        {
            Title = "Games as Art Showcase",
            DateLabel = "Summer 2026",
            Location = "Community gallery",
            Description = "A curated evening celebrating independent game creation, installations, talks, and live demos."
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

    public IReadOnlyCollection<CollectiveProject> GetFeaturedProjects() => FeaturedProjects;

    public IReadOnlyCollection<CollectiveEvent> GetUpcomingEvents() => UpcomingEvents;

    public IReadOnlyCollection<CollectiveMember> GetCollectiveMembers() => CollectiveMembers;
}
