namespace Night.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Image { get; set; }

        public int ReleaseYear { get; set; } 

        public string DeveloperPublisher { get; set; } = string.Empty;

        public Platforms Platforms { get; set; } = Platforms.PC;

        public genreGameplayType GenreGameplayType { get; set; } = genreGameplayType.Action;

        public bool FromCollective{ get; set; }  
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
