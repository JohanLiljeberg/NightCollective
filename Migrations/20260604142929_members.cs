using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class Members : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: " Creator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: "Creator");
        }
    }
}
