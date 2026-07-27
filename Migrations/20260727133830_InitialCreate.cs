using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectiveEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ImageSmallUrl = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    ImageMediumUrl = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    ImageLargeUrl = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
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
                    Position = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    ImageSmallUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageMediumUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLargeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Bio = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    ImageSmallUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageMediumUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLargeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    DeveloperPublisher = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Platforms = table.Column<int>(type: "int", nullable: false),
                    GenreGameplayType = table.Column<int>(type: "int", nullable: false),
                    FromCollective = table.Column<bool>(type: "bit", nullable: false),
                    DeveloperId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectiveMemberGame",
                columns: table => new
                {
                    CollectiveMemberId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveMemberGame", x => new { x.CollectiveMemberId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_CollectiveMemberGame_CollectiveMembers_CollectiveMemberId",
                        column: x => x.CollectiveMemberId,
                        principalTable: "CollectiveMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectiveMemberGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CollectiveEvents",
                columns: new[] { "Id", "Date", "Description", "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl", "IsArchived", "Location", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "A monthly gamejam that anyone can join. New promt everytime!", "/images/events/monthly-gamejam/monthly-gamejam_lg.webp", "/images/events/monthly-gamejam/monthly-gamejam_md.webp", "/images/events/monthly-gamejam/monthly-gamejam_sm.webp", false, "Online + local pop-up", "Monthly Gamejam" },
                    { 2, new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A curated evening celebrating independent game creation, installations, talks, and live demos.", "/images/events/games-as-art-showcase/games-as-art-showcase_lg.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_md.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_sm.webp", false, "Community gallery", "Games as Art Showcase" }
                });

            migrationBuilder.InsertData(
                table: "CollectiveMembers",
                columns: new[] { "Id", "Image", "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl", "Name", "Position", "Quote" },
                values: new object[] { 1, "/images/collective-members/night-collective.jpg", null, null, null, "Night Collective", "Curator", "We champion small teams, expressive play, accessible tools, and games that belong in galleries as much as living rooms." });

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
                columns: new[] { "Id", "Bio", "ImageUrl", "Name", "Role" },
                values: new object[] { 1, "Games are art.", null, "Night Collective", " Creator" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveMemberGame_GamesId",
                table: "CollectiveMemberGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveEvents");

            migrationBuilder.DropTable(
                name: "CollectiveMemberGame");

            migrationBuilder.DropTable(
                name: "CollectiveProjects");

            migrationBuilder.DropTable(
                name: "CollectiveMembers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
