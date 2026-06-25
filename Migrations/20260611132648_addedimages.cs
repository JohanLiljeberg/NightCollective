using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class addedimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Developers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "CollectiveEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageSmallUrl",
                table: "CollectiveEvents",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMediumUrl",
                table: "CollectiveEvents",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageLargeUrl",
                table: "CollectiveEvents",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsArchived", "ImageSmallUrl", "ImageMediumUrl", "ImageLargeUrl" },
                values: new object[] { false, "/images/events/monthly-gamejam/monthly-gamejam_sm.webp", "/images/events/monthly-gamejam/monthly-gamejam_md.webp", "/images/events/monthly-gamejam/monthly-gamejam_lg.webp" });

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsArchived", "ImageSmallUrl", "ImageMediumUrl", "ImageLargeUrl" },
                values: new object[] { false, "/images/events/games-as-art-showcase/games-as-art-showcase_sm.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_md.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_lg.webp" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId",
                table: "Game",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developers_DeveloperId",
                table: "Game",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developers_DeveloperId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageSmallUrl",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageMediumUrl",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageLargeUrl",
                table: "CollectiveEvents");
        }
    }
}
