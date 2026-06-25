using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class Imagesize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CollectiveEvents");

            migrationBuilder.AddColumn<string>(
                name: "ImageLargeUrl",
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
                name: "ImageSmallUrl",
                table: "CollectiveEvents",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl" },
                values: new object[] { "/images/events/monthly-gamejam/monthly-gamejam_lg.webp", "/images/events/monthly-gamejam/monthly-gamejam_md.webp", "/images/events/monthly-gamejam/monthly-gamejam_sm.webp" });

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl" },
                values: new object[] { "/images/events/games-as-art-showcase/games-as-art-showcase_lg.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_md.webp", "/images/events/games-as-art-showcase/games-as-art-showcase_sm.webp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLargeUrl",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageMediumUrl",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageSmallUrl",
                table: "CollectiveEvents");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CollectiveEvents",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/events/monthly-gamejam.svg");

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/events/games-as-art-showcase.svg");
        }
    }
}
