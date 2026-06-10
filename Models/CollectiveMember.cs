namespace Night.Models
{
    public class CollectiveMember
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string Quote { get; set; } = string.Empty;

        public List<Game> Games { get; set; } = new();
    }
}
