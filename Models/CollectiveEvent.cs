namespace Night.Models;

public class CollectiveEvent
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string DateLabel { get; set; }

    public required string Description { get; set; }

    public required string Location { get; set; }
}
