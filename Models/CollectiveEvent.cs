namespace Night.Models;

public class CollectiveEvent
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public DateTime Date { get; set; }

    public required string Location { get; set; }

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }
}
