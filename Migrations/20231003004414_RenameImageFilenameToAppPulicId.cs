using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yelpcamp.Migrations
{
    public partial class RenameImageFilenameToAppPulicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "CampgroundImages",
                newName: "AppPublicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppPublicId",
                table: "CampgroundImages",
                newName: "Filename");
        }
    }
}
