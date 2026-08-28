using Night.Models;

namespace Night.ViewModels;

public class HomeIndexViewModel
{
    public string HeroTitle { get; init; } = "Nightjar Collective";

    public string HeroSubtitle { get; init; } = "Games are art";

    public string HeroDescription { get; init; } = "Become a member today!";

    public IReadOnlyCollection<ProjectCardViewModel> FeaturedProjects { get; init; } = [];

    public IReadOnlyCollection<BlogPost> LatestBlogPosts { get; init; } = [];

    public IReadOnlyCollection<EventViewModel> UpcomingEvents { get; init; } = [];

    public IReadOnlyCollection<CollectiveMemberViewModel> Members { get; init; } = [];
}
