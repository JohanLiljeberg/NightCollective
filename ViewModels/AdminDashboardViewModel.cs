using Night.Models;

namespace Night.ViewModels;

public class AdminDashboardViewModel
{
    public EventFormViewModel EventForm { get; set; } = new();
    public GameFormViewModel GameForm { get; set; } = new();
    public CollectiveMemberFormViewModel MemberForm { get; set; } = new();
    public BlogPostFormViewModel BlogPostForm { get; set; } = new();
    public IReadOnlyCollection<CollectiveMemberViewModel> AllMembers { get; set; } = [];
    public IReadOnlyCollection<GameViewModel> AllGames { get; set; } = [];
    public IReadOnlyCollection<BlogPost> AllBlogPosts { get; set; } = [];
    public IReadOnlyCollection<EventViewModel> AllEvents { get; set; } = [];
}
