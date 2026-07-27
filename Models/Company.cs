namespace Night.Models
{
    public class Company
    {

        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required Game[] Games { get; set; }

        public required Developer[] Developers { get; set; }
    }
}
