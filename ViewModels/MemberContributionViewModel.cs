using Night.Models;

namespace Night.ViewModels;

public class MemberContributionViewModel
{
    public int MemberId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public string? ImageSmallUrl { get; init; }
    public string? ImageMediumUrl { get; init; }
    public string? ImageLargeUrl { get; init; }
    public InvolvementLevel InvolvementLevel { get; init; }
    public List<WorkArea> WorkAreas { get; init; } = new();
}
