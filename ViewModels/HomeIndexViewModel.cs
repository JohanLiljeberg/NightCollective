using Night.Models;

namespace Night.ViewModels;

public class HomeIndexViewModel
{
    public string HeroTitle { get; init; } = "Night Collective";

    public string HeroSubtitle { get; init; } = "Celebrating game creation and games as art.";

    public string HeroDescription { get; init; } = "We are a collective of makers, curators, and players building a home for experimental games, playful installations, and the communities around them.";

    public IReadOnlyCollection<CollectiveProject> FeaturedProjects { get; init; } = [];

    public IReadOnlyCollection<CollectiveEvent> UpcomingEvents { get; init; } = [];

    public IReadOnlyCollection<Developer> Members { get; init; } = [];
}
