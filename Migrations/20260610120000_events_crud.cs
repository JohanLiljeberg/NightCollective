using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class events_crud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLabel",
                table: "CollectiveEvents");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CollectiveEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                columns: new[] { "Date", "ImageUrl" },
                values: new object[] { new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/events/monthly-gamejam.svg" });

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "ImageUrl" },
                values: new object[] { new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/events/games-as-art-showcase.svg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "CollectiveEvents");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CollectiveEvents");

            migrationBuilder.AddColumn<string>(
                name: "DateLabel",
                table: "CollectiveEvents",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateLabel",
                value: "Friday");

            migrationBuilder.UpdateData(
                table: "CollectiveEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateLabel",
                value: "Summer 2026");
        }
    }
}
