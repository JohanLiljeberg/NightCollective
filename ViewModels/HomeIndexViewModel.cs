using Night.Models;

namespace Night.ViewModels;

public class HomeIndexViewModel
{
    public string HeroTitle { get; init; } = "Nightjar Collective";

    public string HeroSubtitle { get; init; } = "Games as art.";

    public string HeroDescription { get; init; } = "Nightjar is a non-profit organization striving to be a democratic game publisher. To be precise, we are a local community of weird game makers who care deeply about the artistry of video games.";

    public IReadOnlyCollection<CollectiveProject> FeaturedProjects { get; init; } = [];

    public IReadOnlyCollection<CollectiveEvent> UpcomingEvents { get; init; } = [];

    public IReadOnlyCollection<CollectiveMember> Members { get; init; } = [];
}
