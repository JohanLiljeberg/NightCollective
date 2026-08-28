using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class AddGameReleaseStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Games",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");
        }
    }
}
