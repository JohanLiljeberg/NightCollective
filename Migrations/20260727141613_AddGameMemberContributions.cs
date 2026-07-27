using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class AddGameMemberContributions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveMemberGame");

            migrationBuilder.CreateTable(
                name: "GameMemberContributions",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    CollectiveMemberId = table.Column<int>(type: "int", nullable: false),
                    InvolvementLevel = table.Column<int>(type: "int", nullable: false),
                    WorkAreas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMemberContributions", x => new { x.CollectiveMemberId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameMemberContributions_CollectiveMembers_CollectiveMemberId",
                        column: x => x.CollectiveMemberId,
                        principalTable: "CollectiveMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMemberContributions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMemberContributions_GameId",
                table: "GameMemberContributions",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMemberContributions");

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

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveMemberGame_GamesId",
                table: "CollectiveMemberGame",
                column: "GamesId");
        }
    }
}
