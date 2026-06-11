using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Night.Migrations
{
    public partial class InitialCreate : Migration
    {
     
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectiveEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DateLabel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectiveMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectiveProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    DeveloperPublisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Platforms = table.Column<int>(type: "int", nullable: false),
                    GenreGameplayType = table.Column<int>(type: "int", nullable: false),
                    FromCollective = table.Column<bool>(type: "bit", nullable: false),
                    CollectiveMemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_CollectiveMembers_CollectiveMemberId",
                        column: x => x.CollectiveMemberId,
                        principalTable: "CollectiveMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CollectiveEvents",
                columns: new[] { "Id", "DateLabel", "Description", "Location", "Title" },
                values: new object[,]
                {
                    { 1, "Friday", "A monthly gamejam that anyone can join. New promt everytime!", "Online + local pop-up", "Monthly Gamejam" },
                    { 2, "Summer 2026", "A curated evening celebrating independent game creation, installations, talks, and live demos.", "Community gallery", "Games as Art Showcase" }
                });

            migrationBuilder.InsertData(
                table: "CollectiveMembers",
                columns: new[] { "Id", "Image", "Name", "Position", "Quote" },
                values: new object[] { 1, "/images/collective-members/night-collective.jpg", "Night Collective", "Curators, developers, artists, and players", "We champion small teams, expressive play, accessible tools, and games that belong in galleries as much as living rooms." });

            migrationBuilder.InsertData(
                table: "CollectiveProjects",
                columns: new[] { "Id", "Creator", "Description", "Medium", "Title" },
                values: new object[,]
                {
                    { 1, "Mira Sol", "A tiny exploration game about mapping memories, procedural stars, and unfinished conversations.", "Interactive poem", "Dream Cartographers" },
                    { 2, "APT Studio", "A cabinet-scale exhibition that treats high scores, rituals, and glitches as community folklore.", "Playable installation", "Arcade Reliquary" },
                    { 3, "Jun Vale", "A non-violent boss rush where every encounter is resolved through rhythm, dialogue, and care.", "Experimental action game", "Soft Boss Rush" }
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Bio", "Name", "Role" },
                values: new object[] { 1, "Games are art.", "Night Collective", "test" });

            migrationBuilder.CreateIndex(
                name: "IX_Game_CollectiveMemberId",
                table: "Game",
                column: "CollectiveMemberId");
        }

  
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveEvents");

            migrationBuilder.DropTable(
                name: "CollectiveProjects");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "CollectiveMembers");
        }
    }
}
