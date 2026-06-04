namespace Night.Models;

public class CollectiveProject
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Creator { get; set; }

    public required string Description { get; set; }

    public required string Medium { get; set; }
}
