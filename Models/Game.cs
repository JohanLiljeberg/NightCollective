using System.ComponentModel.DataAnnotations.Schema;

namespace Night.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        public string? ImageSmallUrl { get; set; }
        public string? ImageMediumUrl { get; set; }
        public string? ImageLargeUrl { get; set; }

        public int ReleaseYear { get; set; }

        public string DeveloperPublisher { get; set; } = string.Empty;

        public Platforms Platforms { get; set; } = Platforms.PC;

        public genreGameplayType GenreGameplayType { get; set; } = genreGameplayType.Action;

        public bool FromCollective { get; set; }

        [NotMapped]
        public int? CollectiveMemberId { get; set; }

        [NotMapped]
        public CollectiveMember? CollectiveMember { get; set; }

        public List<CollectiveMember> Members { get; set; } = new();
    }

    public enum Platforms
    {
        PC,
        Console,
        Mobile,
        VR
    }

    public enum genreGameplayType
    {
        Action,
        Adventure,
        RPG,
        Simulation,
        Strategy,
        Puzzle,
        Sports,
        Horror
    }
}
