namespace Night.ViewModels;

public class GameSelectViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public bool FromCollective { get; init; }
}
