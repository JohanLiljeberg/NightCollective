using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class Imageupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLargeUrl",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ImageMediumUrl",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ImageSmallUrl",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ImageLargeUrl",
                table: "CollectiveMembers");

            migrationBuilder.DropColumn(
                name: "ImageMediumUrl",
                table: "CollectiveMembers");

            migrationBuilder.DropColumn(
                name: "ImageSmallUrl",
                table: "CollectiveMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLargeUrl",
                table: "Game",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMediumUrl",
                table: "Game",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSmallUrl",
                table: "Game",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageLargeUrl",
                table: "CollectiveMembers",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMediumUrl",
                table: "CollectiveMembers",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSmallUrl",
                table: "CollectiveMembers",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CollectiveMembers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl" },
                values: new object[] { null, null, null });
        }
    }
}
