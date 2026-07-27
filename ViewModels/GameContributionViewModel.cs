using Night.Models;

namespace Night.ViewModels;

public class GameContributionViewModel
{
    public int GameId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public int ReleaseYear { get; init; }
    public InvolvementLevel InvolvementLevel { get; init; }
    public List<WorkArea> WorkAreas { get; init; } = new();
}
