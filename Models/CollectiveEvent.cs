namespace Night.Models;

public class CollectiveEvent
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public DateTime Date { get; set; }

    public required string Location { get; set; }

    public required string Description { get; set; }

    // Responsive image URLs (small / medium / large)
    public string? ImageSmallUrl { get; set; }
    public string? ImageMediumUrl { get; set; }
    public string? ImageLargeUrl { get; set; }

    public bool IsArchived { get; set; }
}
