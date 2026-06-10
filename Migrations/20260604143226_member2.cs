using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class member2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CollectiveMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Position",
                value: "Curator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CollectiveMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Position",
                value: "Curators, developers, artists, and players");
        }
    }
}
