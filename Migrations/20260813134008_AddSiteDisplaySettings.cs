using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Night.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteDisplaySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteDisplaySettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowFullMembers = table.Column<bool>(type: "bit", nullable: false),
                    ShowSubscribedMembers = table.Column<bool>(type: "bit", nullable: false),
                    ShowUnsubscribedMembers = table.Column<bool>(type: "bit", nullable: false),
                    ShowCollectiveGames = table.Column<bool>(type: "bit", nullable: false),
                    ShowExternalGames = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteDisplaySettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SiteDisplaySettings",
                columns: new[] { "Id", "ShowCollectiveGames", "ShowExternalGames", "ShowFullMembers", "ShowSubscribedMembers", "ShowUnsubscribedMembers" },
                values: new object[] { 1, true, true, true, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteDisplaySettings");
        }
    }
}
