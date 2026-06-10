using Night.Models;

namespace Night.ViewModels;

public class HomeIndexViewModel
{
    public string HeroTitle { get; init; } = "Nightjar Collective";

    public string HeroSubtitle { get; init; } = "Games as art.";

    public string HeroDescription { get; init; } = "test";

    public IReadOnlyCollection<CollectiveProject> FeaturedProjects { get; init; } = [];

    public IReadOnlyCollection<CollectiveEvent> UpcomingEvents { get; init; } = [];

    public IReadOnlyCollection<CollectiveMember> Members { get; init; } = [];
}
