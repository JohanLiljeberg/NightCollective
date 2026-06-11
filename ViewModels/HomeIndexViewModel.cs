namespace Night.ViewModels;

public class HomeIndexViewModel
{
    public string HeroTitle { get; init; } = "Nightjar Collective";

    public string HeroSubtitle { get; init; } = "Games as art.";

    public string HeroDescription { get; init; } = "test";

    public IReadOnlyCollection<ProjectCardViewModel> FeaturedProjects { get; init; } = [];

    public IReadOnlyCollection<EventViewModel> UpcomingEvents { get; init; } = [];

    public IReadOnlyCollection<CollectiveMemberViewModel> Members { get; init; } = [];
}
