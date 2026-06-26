using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    [Migration("20260626120000_games_member_many_to_many")]
    public partial class games_member_many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Game",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeveloperPublisher",
                table: "Game",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_CollectiveMembers_CollectiveMemberId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_CollectiveMemberId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CollectiveMemberId",
                table: "Game");

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
                        name: "FK_CollectiveMemberGame_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveMemberGame_GamesId",
                table: "CollectiveMemberGame",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveMemberGame");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240);

            migrationBuilder.AlterColumn<string>(
                name: "DeveloperPublisher",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(160)",
                oldMaxLength: 160);

            migrationBuilder.AddColumn<int>(
                name: "CollectiveMemberId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_CollectiveMemberId",
                table: "Game",
                column: "CollectiveMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_CollectiveMembers_CollectiveMemberId",
                table: "Game",
                column: "CollectiveMemberId",
                principalTable: "CollectiveMembers",
                principalColumn: "Id");
        }
    }
}
