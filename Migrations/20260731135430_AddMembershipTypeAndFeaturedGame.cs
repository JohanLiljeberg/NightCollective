using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipTypeAndFeaturedGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeaturedGameId",
                table: "CollectiveMembers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipType",
                table: "CollectiveMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CollectiveMembers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FeaturedGameId", "MembershipType" },
                values: new object[] { null, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveMembers_FeaturedGameId",
                table: "CollectiveMembers",
                column: "FeaturedGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectiveMembers_Games_FeaturedGameId",
                table: "CollectiveMembers",
                column: "FeaturedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectiveMembers_Games_FeaturedGameId",
                table: "CollectiveMembers");

            migrationBuilder.DropIndex(
                name: "IX_CollectiveMembers_FeaturedGameId",
                table: "CollectiveMembers");

            migrationBuilder.DropColumn(
                name: "FeaturedGameId",
                table: "CollectiveMembers");

            migrationBuilder.DropColumn(
                name: "MembershipType",
                table: "CollectiveMembers");
        }
    }
}
