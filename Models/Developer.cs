namespace Night.Models;

public class Developer
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Role { get; set; }

    public required string Bio { get; set; }

    public string? ImageUrl { get; set; }

    public List<Game>? Games { get; set; }
}
