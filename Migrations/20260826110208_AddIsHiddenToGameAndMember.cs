using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class AddIsHiddenToGameAndMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "CollectiveMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CollectiveMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsHidden",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "CollectiveMembers");
        }
    }
}
