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
            // Drop default constraint first (if it exists)
            migrationBuilder.Sql("""
        IF OBJECT_ID('DF__Collectiv__Image__4222D4EF', 'D') IS NOT NULL
            ALTER TABLE [CollectiveEvents] DROP CONSTRAINT [DF__Collectiv__Image__4222D4EF];
        """);

            // Drop column if it still exists
            migrationBuilder.Sql("""
        IF COL_LENGTH(N'CollectiveEvents', N'ImageUrl') IS NOT NULL
        BEGIN
            ALTER TABLE [CollectiveEvents] DROP COLUMN [ImageUrl];
        END
        """);

            // Optional: Update seed data (already in your migration)
            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl" },
                values: new object[] {
            "/images/events/monthly-gamejam/monthly-gamejam_lg.webp",
            "/images/events/monthly-gamejam/monthly-gamejam_md.webp",
            "/images/events/monthly-gamejam/monthly-gamejam_sm.webp"
                });

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageLargeUrl", "ImageMediumUrl", "ImageSmallUrl" },
                values: new object[] {
            "/images/events/games-as-art-showcase/games-as-art-showcase_lg.webp",
            "/images/events/games-as-art-showcase/games-as-art-showcase_md.webp",
            "/images/events/games-as-art-showcase/games-as-art-showcase_sm.webp"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        IF COL_LENGTH(N'CollectiveEvents', N'ImageUrl') IS NULL
        BEGIN
            ALTER TABLE [CollectiveEvents] ADD [ImageUrl] nvarchar(240) NOT NULL 
                CONSTRAINT [DF_CollectiveEvents_ImageUrl] DEFAULT N'';
        END
        """);

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
