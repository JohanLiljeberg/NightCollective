namespace Night.Models;

public class BlogPost
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Author { get; set; }

    public string? Summary { get; set; }

    public required string Content { get; set; }

    public string? Tags { get; set; }

    public string? ExternalLink { get; set; }

    public string? ImageSmallUrl { get; set; }
    public string? ImageMediumUrl { get; set; }
    public string? ImageLargeUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsPublished { get; set; } = true;
}
